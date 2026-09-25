using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Query.Filter
{
    public class BaseFilter
    {
        public int EntityCount { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageCount { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int Limit { get; private set; }

        public void GeneratePaging(IQueryable<Object> data, int limit, int currentPage)
        {
            var entityCount = data.Count();
            GeneratePaging(entityCount, limit, currentPage);
        }

        public void GeneratePaging(int entityCount, int limit, int currentPage)
        {
            EntityCount = entityCount;
            var pageCount = (int)Math.Ceiling(entityCount / (double)limit);
            PageCount = pageCount;
            CurrentPage = currentPage;
            EndPage = (currentPage + 5 > pageCount) ? pageCount : currentPage + 5;
            Limit = limit;
            StartPage = (currentPage - 4 <= 0) ? 1 : currentPage - 4;
        }

        public class BaseFilterParam
        {
            public int PageId { get; set; } = 1;
            public int Limit { get; set; } = 10;
        }

        public class BaseFilterGeneric<TData, TParam> : BaseFilter
            where TParam : BaseFilterParam
            where TData : BaseDto
        {
            public List<TData> Data { get; set; }
            public TParam FilterParam { get; set; }
        }

        // Backwards-compatible nested generic type name used across the codebase
    }

    // Backwards-compatible generic type at namespace level: BaseFilter<TData,TParam>
    public class BaseFilter<TData, TParam> : BaseFilter.BaseFilterGeneric<TData, TParam>
        where TParam : BaseFilter.BaseFilterParam
        where TData : BaseDto
    {
    }

}
