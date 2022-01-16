using Books.Dal.Repository.Interfaces;
using Books.Models;
using Books.Models.ViewModels;
using Books.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace Books.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// 
        /// </summary>
        [BindProperty]
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="emailSender"></param>
        public CartController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ClaimsIdentity? claimsIdentity = (ClaimsIdentity)User.Identity;
            Claim? claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartViewModel = new ShoppingCartViewModel()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Product"),
                OrderHeader = new()
            };

            foreach (ShoppingCart? cart in ShoppingCartViewModel.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
                    cart.Product.Price50, cart.Product.Price100);
                ShoppingCartViewModel.OrderHeader.OrderTotal += cart.Price * cart.Count;
            }
            return View(ShoppingCartViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public IActionResult Plus(int cartId)
        {
            ShoppingCart? cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public IActionResult Minus(int cartId)
        {
            ShoppingCart? cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
                //var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;
                //HttpContext.Session.SetInt32(Constants.SessionCart, count);
            }
            else
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public IActionResult Remove(int cartId)
        {
            ShoppingCart? cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            int count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            //HttpContext.Session.SetInt32(Constants.SessionCart, count);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Summary()
        {
            ClaimsIdentity? claimsIdentity = (ClaimsIdentity)User.Identity;
            Claim? claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartViewModel = new ShoppingCartViewModel()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Product"),
                OrderHeader = new()
            };

            ShoppingCartViewModel.OrderHeader.ApplicationUser =
                _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

            ShoppingCartViewModel.OrderHeader.Name = ShoppingCartViewModel.OrderHeader.ApplicationUser.Name;
            ShoppingCartViewModel.OrderHeader.PhoneNumber = ShoppingCartViewModel.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartViewModel.OrderHeader.StreetAddress = ShoppingCartViewModel.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartViewModel.OrderHeader.City = ShoppingCartViewModel.OrderHeader.ApplicationUser.City;
            ShoppingCartViewModel.OrderHeader.State = ShoppingCartViewModel.OrderHeader.ApplicationUser.State;
            ShoppingCartViewModel.OrderHeader.PostalCode = ShoppingCartViewModel.OrderHeader.ApplicationUser.PostalCode;

            foreach (ShoppingCart? cart in ShoppingCartViewModel.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
                    cart.Product.Price50, cart.Product.Price100);

                ShoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(ShoppingCartViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost()
        {
            ClaimsIdentity? claimsIdentity = (ClaimsIdentity)User.Identity;
            Claim? claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartViewModel.ListCart =
                _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Product");

            ShoppingCartViewModel.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartViewModel.OrderHeader.ApplicationUserId = claim.Value;


            foreach (ShoppingCart? cart in ShoppingCartViewModel.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
                    cart.Product.Price50, cart.Product.Price100);
                ShoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                ShoppingCartViewModel.OrderHeader.PaymentStatus = Constants.PaymentStatusPending;
                ShoppingCartViewModel.OrderHeader.OrderStatus = Constants.StatusPending;
            }
            else
            {
                ShoppingCartViewModel.OrderHeader.PaymentStatus = Constants.PaymentStatusDelayedPayment;
                ShoppingCartViewModel.OrderHeader.OrderStatus = Constants.StatusApproved;
            }

            _unitOfWork.OrderHeader.Add(ShoppingCartViewModel.OrderHeader);
            _unitOfWork.Save();
            foreach (ShoppingCart? cart in ShoppingCartViewModel.ListCart)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderId = ShoppingCartViewModel.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                //stripe settings 
                string? uri = "https://localhost:44321/";
                SessionCreateOptions? options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = uri + $"customer/cart/OrderConfirmation?id={ShoppingCartViewModel.OrderHeader.Id}",
                    CancelUrl = uri + $"customer/cart/index"
                };

                foreach (ShoppingCart? item in ShoppingCartViewModel.ListCart)
                {
                    SessionLineItemOptions? sessionlineitem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),//20.00 -> 2000
                            Currency = "inr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Title
                            }
                        },
                        Quantity = item.Count,
                    };
                    options.LineItems.Add(sessionlineitem);
                }

                SessionService? service = new SessionService();
                Session session = service.Create(options);

                _unitOfWork.OrderHeader.UpdateStripePaymentID(ShoppingCartViewModel.OrderHeader.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
            else
            {
                return RedirectToAction("OrderConfirmation", "Cart", new
                {
                    id = ShoppingCartViewModel.OrderHeader.Id
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser");
            if (orderHeader.PaymentStatus != Constants.PaymentStatusDelayedPayment)
            {
                SessionService? service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                //check the stripe status
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeader.UpdateStatus(id, Constants.StatusApproved, Constants.PaymentStatusApproved);
                    _unitOfWork.Save();
                }
            }
            _emailSender.SendEmailAsync(orderHeader.ApplicationUser.Email, "New Order - Bulky Book", "<p>New Order Created</p>");

            //Remove once the order is placed
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart
                .GetAll(u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();

            return View(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <param name="price50"></param>
        /// <param name="price100"></param>
        /// <returns></returns>
        private double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
        {
            if (quantity <= 50)
            {
                return price;
            }
            else
            {
                if (quantity <= 100)
                {
                    return price50;
                }
                return price100;
            }
        }
    }
}
