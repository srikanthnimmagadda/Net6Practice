using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
    public class Product
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string ISBN { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Author { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Range(1, 10000)]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 1-50")]
        public double Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 51-100")]
        public double Price50 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ValidateNever]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name = "Cover Type")]
        public int CoverTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ValidateNever]
        public CoverType CoverType { get; set; }
    }
}
