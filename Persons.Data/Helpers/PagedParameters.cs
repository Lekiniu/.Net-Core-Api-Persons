using System;
using System.Collections.Generic;
using System.Text;

namespace Persons.Data.Helpers
{
    public class PagedParameters
    {
        const int maxPageSize = 10;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string SearchQuery { get; set; } = "";

        public string SortOrder { get; set; } = "";
    }
}
