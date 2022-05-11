using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.Infrastructure.Domain.Pagination
{
    public class PaginationResponse<TItem> : IPaginationResponse<TItem>
    {
        public PaginationResponse(int pageSize, int currentPage, int totalItemsCount, IEnumerable<TItem> items)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalItemsCount = totalItemsCount;
            TotalPageCount = totalItemsCount % pageSize == 0 ? totalItemsCount / pageSize : totalItemsCount / pageSize + 1;
            HasPerviousPage = CurrentPage > 1;
            HasNextPage = CurrentPage + 1 < TotalPageCount;
            Items = items;
        }

        public int PageSize { get; protected set; }
        public int CurrentPage { get; protected set; }
        public int TotalPageCount { get; protected set; }
        public int TotalItemsCount { get; protected set; }
        public bool HasPerviousPage { get; protected set; }
        public bool HasNextPage { get; protected set; }
        public IEnumerable<TItem> Items { get; protected set; }
    }
}
