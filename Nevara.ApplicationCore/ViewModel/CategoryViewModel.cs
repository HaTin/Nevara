using System.Collections.Generic;
using Nevara.ApplicationCore.Models.Entities;

namespace Nevara.ApplicationCore.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { set; get; }

    }
}
