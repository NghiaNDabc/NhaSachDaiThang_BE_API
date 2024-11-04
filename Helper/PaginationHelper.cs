﻿namespace NhaSachDaiThang_BE_API.Helper
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public static class PaginationHelper
    {
        public static async Task<IEnumerable<T>> PaginateAsync<T>(IQueryable<T> query, int? pageNumber = null, int? pageSize = null)
        {
            if (pageNumber == null || pageNumber <= 0 || pageSize == null || pageSize <= 0)
            {
                return await query.ToListAsync();
            }

            int defaultPageSize = 10;
            int pageNum = pageNumber ?? 1;
            int size = pageSize ?? defaultPageSize;

            int skip = (pageNum - 1) * size;
            return await query.Skip(skip).Take(size).ToListAsync();
        }
    }

}