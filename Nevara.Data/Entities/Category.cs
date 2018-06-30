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
    [Table("Categories")]
    public class Category : BaseEntity,ISwitchable,IRemovable
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Image { get; set; }
        public Status Status { get; set; }
        public bool? IsRemoved { get; set; }
        public virtual ICollection<Product> Products { set; get; }

    }
}
