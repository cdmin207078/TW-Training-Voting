using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.Infrastructure.Domain.Pagination
{
    public interface IPaginationRequest
    {
        public int PageSize { get; }
        public int CurrentPage { get; }
    }
}
