using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
    public class ShoppingCart
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; }

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
        [NotMapped]
        public double Price { get; set; }
    }
}
