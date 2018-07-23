using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nevara.ApplicationCore.Models.Enum;

namespace Nevara.ApplicationCore.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; } 
        public string CustomerName { get; set; }  
        public string CustomerMobile { get; set; }    
        public string CustomerAddress { get; set; }
        public string CustomerMessage { get; set; }
        public string CustomerEmail { get; set; }
        public Guid? UserId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMMM dd,yyyy}")]
        public DateTime CreatedDate { get; set; }
        public BillStatus BillStatus { get; set; }
        public IList<OrderDetailViewModel> DetailViewModels { get; set; }        
    }
}
