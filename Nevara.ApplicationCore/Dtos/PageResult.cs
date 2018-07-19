using System.Collections.Generic;

namespace Nevara.ApplicationCore.Dtos
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
