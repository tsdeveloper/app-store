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
        
        Task<IReadOnlyCollection<ProductBrand>> GetProductBrandAllAsync (int page = 1, int pageTotal = 10, 
            Expression<Func<ProductBrand, bool>> filter = null, 
            Expression<Func<IQueryable<ProductBrand>,IOrderedQueryable<ProductBrand>>> orderFilter = null, 
            params Expression<Func<ProductBrand, object>>[] includes);
        
        
        Task<IReadOnlyCollection<ProductType>> GetProductTypeAllAsync (int page = 1, int pageTotal = 10, 
            Expression<Func<ProductType, bool>> filter = null, 
            Expression<Func<IQueryable<ProductType>,IOrderedQueryable<ProductType>>> orderFilter = null, 
            params Expression<Func<ProductType, object>>[] includes);
    }
}