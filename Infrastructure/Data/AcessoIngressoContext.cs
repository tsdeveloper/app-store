using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AcessoIngressoContext : DbContext
    {
        public AcessoIngressoContext(DbContextOptions options) : base(options)
        {
        }

       
        protected override void OnModelCreating(ModelBuilder m)
        {
            base.OnModelCreating(m);
            m.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in m.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties()
                        .Where(p => p.PropertyType.Equals(typeof(decimal)));

                    foreach (var property in properties)
                    {
                        m.Entity(entityType.Name).Property(property.Name)
                            .HasConversion<double>();
                    }
                }
            }
        }

}

    public static class HackyDbSetGetContextTrick
    {

        public static DbContext GetContext<TEntity>(this DbSet<TEntity> dbSet)
            where TEntity : class
        {
            object internalSet = dbSet
                .GetType()
                .GetField("_internalSet", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(dbSet);
            object internalContext = internalSet
                .GetType()
                .BaseType
                .GetField("_internalContext", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(internalSet);
            return (DbContext)internalContext
                .GetType()
                .GetProperty("Owner", BindingFlags.Instance | BindingFlags.Public)
                .GetValue(internalContext, null);
        }

    }
    
    public static class AcessoIngressoContextExtensions {
        public static DbSet<TEntityType> DbSet<TEntityType> (this AcessoIngressoContext context)
            where TEntityType : class {
            return context.Set<TEntityType> ();
        }
    }
}