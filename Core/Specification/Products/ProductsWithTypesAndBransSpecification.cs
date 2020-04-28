using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification.Products
{
    public class ProductsWithTypesAndBransSpecification: BaseSpecification<Product>
    {
        public ProductsWithTypesAndBransSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        public ProductsWithTypesAndBransSpecification(Guid id, bool isCanceled = false) : 
            base(x => x.Id.Equals(id) && x.IsCanceled.Equals(isCanceled))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}