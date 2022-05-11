using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.Infrastructure.Domain.Pagination
{
    public interface IPaginationResponse<TItem>
    {
        public int PageSize { get; }
        public int CurrentPage { get; }
        public int TotalPageCount { get; }
        public int TotalItemsCount { get; }
        public bool HasPerviousPage { get; }
        public bool HasNextPage { get; }
        public IEnumerable<TItem> Items { get; }
    }
}
