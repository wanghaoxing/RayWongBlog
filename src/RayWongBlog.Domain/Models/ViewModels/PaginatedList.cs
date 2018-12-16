using System;
using System.Collections.Generic;

namespace RayWongBlog.Domain.Models.ViewModels
{
    public class PaginatedList<T> : List<T> where T : class
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalCount { get; set; }

        public int PageCount
        {
            get
            {
                return Convert.ToInt32(Math.Ceiling((double)TotalCount / (double)PageSize));
            }
        }

        public bool GetHasPrevious
        {
            get
            {
                return PageIndex > 0;
            }

        }

        public bool GetHasNext
        {
            get
            {
                return PageIndex < PageCount - 1;
            }

        }

        public PaginatedList(int pageIndex, int pageSize, int totalCount, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            AddRange(data);
        }
    }
}
