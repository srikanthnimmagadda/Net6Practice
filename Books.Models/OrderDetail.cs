using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
    public class OrderDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("OrderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Price { get; set; }
    }
}
