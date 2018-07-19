using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nevara.ApplicationCore.Models.Interfaces;

namespace Nevara.ApplicationCore.Models.Entities
{
    [Table("Collections")]
    public class Collection : IHasSoftDelete
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string CollectionName { get; set; }  
        public string Description { get; set; }    
        [StringLength(50)]
        public string Image { get; set; }        
        public ICollection<Product> Products { get; set; }
        public bool IsDeleted { get; set; }
    }
}
