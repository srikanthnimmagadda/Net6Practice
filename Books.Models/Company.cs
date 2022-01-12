using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class Company
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

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
        public string? PhoneNumber { get; set; }
    }
}
