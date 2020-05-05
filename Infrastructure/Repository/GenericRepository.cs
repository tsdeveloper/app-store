using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AcessoIngressoContext _context;

        public GenericRepository(AcessoIngressoContext context)
        {
            _context = context;
        }
        public async Task<T> GetByIdAsync(Guid id, Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T> ();
            try {
                foreach (var include in includes)
                    query = query.Include (include);

                if (filter != null)
                    query = query.Where (filter);

                return await query.FirstOrDefaultAsync (x => x.Id.Equals(id));

            } catch (Exception e) {

                Console.WriteLine (e);
            }

            return null;
        }

        public async Task<IReadOnlyCollection<T>> GetTAllAsync(int page = 1, int pageTotal = 10, Expression<Func<T, bool>> filter = null, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderFilter = null,
            params Expression<Func<T, object>>[] includes)
        {
             
            IQueryable<T> query = _context.Set<T> ();

            try {
                foreach (var include in includes)
                    query = query.Include (include);

                if (filter != null)
                    query = query.Where (filter);

                if (orderFilter != null)
                    query = orderFilter.Compile () (query);

                return await query.Skip((page - 1) * pageTotal).Take(pageTotal).ToArrayAsync ();

            } catch (Exception e) {

                Console.WriteLine (e);
            }

            return null;
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}