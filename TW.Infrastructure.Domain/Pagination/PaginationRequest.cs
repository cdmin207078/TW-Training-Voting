using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.Infrastructure.Domain.Pagination
{
    public abstract class PaginationRequest : IPaginationRequest
    {
        protected PaginationRequest(int pageSize, int currentPage)
        {
            if (pageSize < 1)
                throw new ArgumentException("pageSize is error value");
            if (currentPage < 1)
                throw new ArgumentException("currentPage is error value");

            PageSize = pageSize;
            CurrentPage = currentPage;
        }

        public int PageSize { get; protected set; }
        public int CurrentPage { get; protected set; }
    }
}
