using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? StreetAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
    }
}
