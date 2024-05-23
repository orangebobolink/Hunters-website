using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Modules.Document.Infrastructure.Extenstions
{
    internal static class EFCoreExtension
    {
        public static IQueryable<TEntity> TrackChanges<TEntity>(
            this IQueryable<TEntity> query,
            bool trackChanges)
            where TEntity : class
        {
            if (trackChanges)
                query.AsNoTracking();

            return query;
        }

        public static IQueryable<TEntity> ExceptIncludes<TEntity>(
            this IQueryable<TEntity> query,
            bool exceptIncludes)
            where TEntity : class
        {
            var projectedQuery =
              exceptIncludes
              ? query.ProjectToType<TEntity>()
              : query as IQueryable<TEntity>;

            return projectedQuery;
        }
    }
}
