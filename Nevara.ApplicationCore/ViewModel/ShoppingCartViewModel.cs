using System.Xml.Schema;

namespace Nevara.ApplicationCore.ViewModel
{
    public class ShoppingCartViewModel
    {
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal GetTotal => Quantity * Price;
    }
}
