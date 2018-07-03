using System.Collections.Generic;
using Nevara.Models.Entities;

namespace Nevara.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { set; get; }

    }
}
