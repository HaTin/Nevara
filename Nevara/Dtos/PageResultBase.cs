using System;

namespace Nevara.Dtos
{
    public class PageResultBase
    {
        public int CurrentPage { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (double)RowCount / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
            set => PageCount = value;
        }

        public int PageSize { get; set; }
        public int RowCount { get; set; }
    }
}
