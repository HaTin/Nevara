using System.Collections.Generic;

namespace Nevara.ApplicationCore.ViewModel
{
    public class NavViewModel
    {
        public List<CategoryViewModel> Categories { set; get; }
        public List<CollectionViewModel> Collections { set; get; }   
    }
}