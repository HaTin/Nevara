using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Models.Interfaces;

namespace Nevara.Models.Entities
{
    [Table("Materials")]
    public class Material : IHasSoftDelete
    {    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }        
    [Required]
    [StringLength(255)]
    public string MaterialName { get; set; }
    public bool IsDeleted { get; set; }
    }
}
