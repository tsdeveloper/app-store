using System;
using Core.Entities;

namespace Core.Specification.Products.SpecParams
{
    public class ProductSpecParams : BaseSpecParams
    {
        
        public Guid? BrandId { get; set; }
        public Guid? TypeId { get; set; }
      
    }
}