using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AdminDashboard.src.Configs.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.src.Utilities
{
    public static class DbContextExtensions
{
    public static async Task EnsureUniqueAsync<T>(
        this DbContext context,
        Expression<Func<T, bool>> predicate,
        string conflictMessage) where T : class
    {
        var exists = await context.Set<T>().AsNoTracking().AnyAsync(predicate);
        if (exists)
        {
            throw new ConflictException(conflictMessage);
        }
    }
}

}