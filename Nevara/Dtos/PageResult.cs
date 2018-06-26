using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nevara.Dtos
{
    public class PageResult<T> : PageResultBase where T : class
    {
        public IList<T> Results { get; set; }
        public PageResult()
        {
            Results = new List<T>();
        }
    }
}
