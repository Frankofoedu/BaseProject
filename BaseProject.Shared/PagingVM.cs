using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.ViewModels
{
    public class PagingVM
    {
        public int? PageNumber { get; set; }

        public int PageSize { get; set; } = 10;
        public string SortField { get; set; }

        public string SortOrder { get; set; }
    }
}