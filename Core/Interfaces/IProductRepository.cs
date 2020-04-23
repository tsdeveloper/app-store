using System.Linq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces {
    public interface IProductRepository {
        Task<IReadOnlyCollection<Product>> GetProductAllAsync (int page = 1, int pageTotal = 10, Expression<Func<Product, bool>> filter = null, 
                                        Expression<Func<IQueryable<Product>,IOrderedQueryable<Product>>> orderFilter = null, 
                                        params Expression<Func<Product, object>>[] includes);
        Task<Product> GetProductByIdAsync (Expression<Func<Product, bool>> filter = null, 
                                        params Expression<Func<Product, object>>[] includes);
    }
}