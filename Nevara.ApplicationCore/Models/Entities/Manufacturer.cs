using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Nevara.ApplicationCore.Models.Interfaces;

namespace Nevara.ApplicationCore.Models.Entities
{
    [Table("Manufacturers")]
    public class Manufacturer : IHasSoftDelete
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ManufacturerName { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
