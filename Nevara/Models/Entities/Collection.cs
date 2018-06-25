using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Models.Interfaces;

namespace Nevara.Models.Entities
{
    public class Collection : IHasSoftDelete
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string CollectionName { get; set; }  
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        public string Image { get; set; }        
        public ICollection<Product> Products { get; set; }
        public bool IsDeleted { get; set; }
    }
}
