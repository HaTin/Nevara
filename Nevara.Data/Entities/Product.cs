using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Nevara.Data.Enum;
using Nevara.Data.Interfaces;
using Nevara.Data.SharedKernel;

namespace Nevara.Data.Entities
{
    [Table("Products")]
    public class Product : BaseEntity,ISwitchable,IRemovable
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int? Quantity { get; set; }
        [Required]
        [StringLength(20)]
        public string Unit { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? NewFlag { get; set; }
        public bool? HotFlag { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public Status Status { get; set; }
        public bool? IsRemoved { get; set; }

    }
}
