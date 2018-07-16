using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Models.Entities;
using Nevara.Models.Enum;

namespace Nevara.ViewModel
{
    public class OrderDetailViewModel
    {      
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }        
        public decimal Price { set; get; }
        public Order Order { get; set; }
    }
}
