using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nevara.Models.Interfaces;

namespace Nevara.Models.Entities
{
    [Table("Colors")]
    public class Color : IHasSoftDelete
    {
  
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        
        public string ColorName { get; set; }        
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Code { get; set; }
        ICollection<Product> Products { get; set; }
        public bool IsDeleted { get; set; }
    }
}
