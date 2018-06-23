using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nevara.Models.Entities
{
    public class OrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        
        public decimal Price { set; get; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
