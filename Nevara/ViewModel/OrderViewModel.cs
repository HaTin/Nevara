using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Models.Entities;
using Nevara.Models.Enum;

namespace Nevara.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }
 
        public string CustomerName { get; set; }  
        public string CustomerMobile { get; set; }    
        public string CustomerAddress { get; set; }
        public string CustomerMessage { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public BillStatus BillStatus { get; set; }

    }
}
