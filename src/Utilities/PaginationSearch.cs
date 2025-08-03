using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Utilities
{
    public class PaginationSearch
    {
        public static async Task<PaginationResult<T>> PaginationAsync<T>(List<T> toUseList, int pageNumber, int pageSize)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;
            return new PaginationResult<T>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalItems = (int)Math.Ceiling(toUseList.Count() / (double)pageSize),
                Items = await Task.Run(() => toUseList.Skip(itemsToSkip).Take(pageSize).ToList())
            };
        }
    }
}