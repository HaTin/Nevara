using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nevara.Models.Entities
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        
        public decimal Price { set; get; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
