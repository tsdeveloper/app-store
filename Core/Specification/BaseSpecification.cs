﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> crieria)
        {
            Crieria = crieria;
            
        }

        public Expression<Func<T, bool>> Crieria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = 
            new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}