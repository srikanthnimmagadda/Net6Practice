namespace Books.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ShoppingCart> ListCart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderHeader OrderHeader { get; set; }
    }
}
