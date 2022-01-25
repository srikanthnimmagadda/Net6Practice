namespace Books.Models
{
    public class PagingInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int TotalItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UrlParam { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalPage => (int)Math.Ceiling((decimal)TotalItem / ItemsPerPage);
    }
}
