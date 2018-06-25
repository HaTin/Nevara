using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Models.Enum;
using Nevara.Models.Interfaces;

namespace Nevara.Models.Entities
{
    [Table("Orders")]
    public class Order : IHasSoftDelete
    {
       
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(20)]
        public string CustomerMobile { get; set; }
        [StringLength(255)]
        [Required]        
        public string CustomerAddress { get; set; }
        public string CustomerMessage { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public BillStatus BillStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("CustomerId")]
        public AppUser User { set; get; }
        public bool IsDeleted { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
