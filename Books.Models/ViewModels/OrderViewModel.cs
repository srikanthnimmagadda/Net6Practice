namespace Books.Models.ViewModels
{
    public class OrderViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public OrderHeader OrderHeader { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
