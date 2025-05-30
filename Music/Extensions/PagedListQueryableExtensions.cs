using Microsoft.EntityFrameworkCore;

namespace Music.Extensions;

public static class PagedListQueryableExtensions
{
    public static async Task<List<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int page,
        int pageSize)
    {
        var items = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
 
        return items;
    }
}