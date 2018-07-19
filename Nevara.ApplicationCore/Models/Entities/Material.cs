using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nevara.ApplicationCore.Models.Interfaces;

namespace Nevara.ApplicationCore.Models.Entities
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
