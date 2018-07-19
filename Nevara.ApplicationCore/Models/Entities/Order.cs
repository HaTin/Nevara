using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nevara.ApplicationCore.Models.Enum;
using Nevara.ApplicationCore.Models.Interfaces;

namespace Nevara.ApplicationCore.Models.Entities
{
    [Table("Orders")]
    public class Order : IHasSoftDelete
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public string CustomerEmail { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public BillStatus BillStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { set; get; }
        public bool IsDeleted { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
