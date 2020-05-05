﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specification;


namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync (Guid id, Expression<Func<T, bool>> filter = null, 
            params Expression<Func<T, object>>[] includes);
        
        Task<IReadOnlyCollection<T>> GetTAllAsync (int page = 1, int pageTotal = 10, 
            Expression<Func<T, bool>> filter = null, 
            Expression<Func<IQueryable<T>,IOrderedQueryable<T>>> orderFilter = null, 
            params Expression<Func<T, object>>[] includes);

        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}