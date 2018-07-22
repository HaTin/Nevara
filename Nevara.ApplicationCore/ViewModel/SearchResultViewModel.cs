using System.Collections.Generic;
using Nevara.ApplicationCore.Dtos;

namespace Nevara.ApplicationCore.ViewModel
{
    public class SearchResultViewModel
    {
        public PageResult<ProductViewModel> Data { get; set; }

        public List<CategoryViewModel> Category { set; get; }
        public string Keyword { get; set; }
    }
}