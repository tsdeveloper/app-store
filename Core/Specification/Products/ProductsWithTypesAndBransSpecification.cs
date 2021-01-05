using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specification.Products.SpecParams;

namespace Core.Specification.Products
{
    public class ProductsWithTypesAndBransSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBransSpecification(ProductSpecParams productParams)
            : base(x =>
                (productParams.Id == 0  || x.Id.Equals(productParams.Id)) &&
                (string.IsNullOrWhiteSpace(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)
                                                                 || x.Name.ToLower().StartsWith(productParams.Search) ||
                                                                 x.Name.ToLower().EndsWith(productParams.Search) ||
                                                                 x.Name.ToLower().Equals(productParams.Search)
                ) &&
                (!productParams.BrandId.HasValue || x.ProductBrandId.Equals(productParams.BrandId)) &&
                (!productParams.TypeId.HasValue || x.ProductTypeId.Equals(productParams.TypeId)) &&
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

            ;
        }

        public ProductsWithTypesAndBransSpecification(ProductSpecParams id, bool isCanceled = false) :
            base(x => x.Id.Equals(id) && x.IsCanceled.Equals(isCanceled))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}