﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Crieria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        
    }
}