using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification.Products
{
    public class ProductsWithTypesAndBransSpecification: BaseSpecification<Product>
    {
        public ProductsWithTypesAndBransSpecification(string sort, Guid? brandId, Guid? typeId)
        : base(x =>
                (!brandId.HasValue || x.ProductBrandId.Equals(brandId)) &&
                (!typeId.HasValue || x.ProductTypeId.Equals(typeId))
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderby(x => x.Name);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "priceAsc" :
                        AddOrderby(p => p.Price);
                        break;
                    case "priceDesc" :
                        AddOrderbyByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderby(p => p.Name);
                        break;
                        
                }

            };
        }

        public ProductsWithTypesAndBransSpecification(Guid id, bool isCanceled = false) : 
            base(x => x.Id.Equals(id) && x.IsCanceled.Equals(isCanceled))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}