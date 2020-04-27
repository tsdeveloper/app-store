using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository {
    public class ProductRepository : IProductRepository {
        private readonly AcessoIngressoContext _context;
        protected DbSet<Product> DbSet;

        public ProductRepository (AcessoIngressoContext context) {
            _context = context;
            DbSet = _context.Set<Product> ();

        }
        public async Task<IReadOnlyCollection<Product>> GetProductAllAsync (int page = 1, int pageTotal = 10, Expression<Func<Product, bool>> filter = null,
            Expression<Func<IQueryable<Product>, IOrderedQueryable<Product>>> orderFilter = null,
            params Expression<Func<Product, object>>[] includes) {
            
            IQueryable<Product> query = _context.Set<Product> ();

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

        public async Task<Product> GetProductByIdAsync (Expression<Func<Product, bool>> filter = null,
                            params Expression<Func<Product, object>>[] includes) {
            
            IQueryable<Product> query = _context.Set<Product> ();
             try {
                foreach (var include in includes)
                    query = query.Include (include);

                if (filter != null)
                    query = query.Where (filter);

                return await query.FirstOrDefaultAsync ();

            } catch (Exception e) {

                Console.WriteLine (e);
            }

            return null;
        }

        public async Task<IReadOnlyCollection<ProductBrand>> GetProductBrandAllAsync(int page = 1, int pageTotal = 10, Expression<Func<ProductBrand, bool>> filter = null, Expression<Func<IQueryable<ProductBrand>, IOrderedQueryable<ProductBrand>>> orderFilter = null,
            params Expression<Func<ProductBrand, object>>[] includes)
        {
            
            IQueryable<ProductBrand> query = _context.Set<ProductBrand> ();

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

        public async Task<IReadOnlyCollection<ProductType>> GetProductTypeAllAsync(int page = 1, int pageTotal = 10, Expression<Func<ProductType, bool>> filter = null, Expression<Func<IQueryable<ProductType>, IOrderedQueryable<ProductType>>> orderFilter = null,
            params Expression<Func<ProductType, object>>[] includes)
        {
             
            IQueryable<ProductType> query = _context.Set<ProductType> ();

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
    }
}