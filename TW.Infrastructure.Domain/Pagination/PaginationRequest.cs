using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW.Infrastructure.Core.Exceptions;

namespace TW.Infrastructure.Domain.Pagination
{
    public abstract class PaginationRequest : IPaginationRequest
    {
        protected PaginationRequest(int pageSize, int currentPage)
        {
            if (pageSize < 1)
                throw new TWException("pageSize is error value");
            if (currentPage < 1)
                throw new TWException("currentPage is error value");

            PageSize = pageSize;
            CurrentPage = currentPage;
        }

        public int PageSize { get; protected set; }
        public int CurrentPage { get; protected set; }
    }
}
