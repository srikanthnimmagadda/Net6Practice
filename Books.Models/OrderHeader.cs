using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
    public class OrderHeader
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ShippingDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double OrderTotal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? OrderStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? PaymentStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? TrackingNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Carrier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PaymentDueDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? SessionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? PaymentIntentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string StreetAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string PostalCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
