namespace Books.Models.ViewModels
{
    public class CategoryViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Category> Categories { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
