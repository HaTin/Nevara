using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nevara.Areas.Admin.Models
{
    public class CollectionViewModel
    { 
        public int Id { get; set; } 
        public string CollectionName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
