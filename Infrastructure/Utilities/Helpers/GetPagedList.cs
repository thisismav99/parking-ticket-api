using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Utilities.Helpers
{
    internal static class GetPagedList<T> where T : class
    {
        public static async Task<List<T>> GetList(IQueryable<T> query, int pageNumber, int pageSize)
        {
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
