using System;

namespace Nevara.ApplicationCore.ViewModel
{
    public class OrderDetailViewModel
    {      
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }        
        public int ProductId { get; set; }        
        public decimal Price { set; get; }
        public OrderViewModel OrderViewModel { get; set; }
        public ProductViewModel ProductViewModel { get; set; }     
    }
}
