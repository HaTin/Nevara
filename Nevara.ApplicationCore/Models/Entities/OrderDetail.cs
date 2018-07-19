using System.ComponentModel.DataAnnotations.Schema;

namespace Nevara.ApplicationCore.Models.Entities
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }        
        public int ProductId { get; set; }        
        public decimal Price { set; get; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }
    }
}
