using Nevara.Data.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nevara.Data.Entities
{
    [Table("Colors")]
    public class Color : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string ColorName { get; set; }
        public int MyProperty { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        ICollection<Product> Products { get; set; }
    }
}
