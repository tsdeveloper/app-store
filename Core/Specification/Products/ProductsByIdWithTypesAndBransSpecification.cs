using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specification.Products.SpecParams;

namespace Core.Specification.Products
{
    public class ProductsByIdWithTypesAndBransSpecification : BaseSpecification<Product>
    {
        public ProductsByIdWithTypesAndBransSpecification(ProductSpecParams productParams)
            : base(x =>
                x.Id.Equals(productParams.Id) &&
                x.IsCanceled.Equals(false)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderby(x => x.Name);

            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrWhiteSpace(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderby(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderbyByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderby(p => p.Name);
                        break;
                }
            }
        }
    }
}