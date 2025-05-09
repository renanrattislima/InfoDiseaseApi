namespace Infrastructure.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using System.Linq;

    public static class DbContextExtensions
    {
        public static IQueryable<TEntity> IncludeRelatedEntities<TEntity>(this IQueryable<TEntity> query, DbContext context) where TEntity : class
        {
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            if (entityType == null) return query;

            var navigations = entityType.GetNavigations();

            // Inclui todas as navegações (propriedades de navegação) da entidade
            foreach (var navigation in navigations)
            {
                query = query.Include(navigation.Name);
            }

            return query;
        }
    }

}
