using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Nevara.Models.Interfaces;

namespace Nevara.Models.Entities
{
    public class Manufacturer : IHasSoftDelete
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string ManufacturerName { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
