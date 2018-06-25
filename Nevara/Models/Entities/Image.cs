using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nevara.Models.Entities
{
    [Table("Images")]
    public class Image
    {
   
        public int Id { get; set; }
        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string ImagePath { get; set; }
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
