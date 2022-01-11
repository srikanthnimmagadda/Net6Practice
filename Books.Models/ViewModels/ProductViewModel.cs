using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Books.Models.ViewModels
{
    public class ProductViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypes { get; set; }
    }
}
