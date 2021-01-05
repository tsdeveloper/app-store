using Core.Entities;
using Core.Specification.Products.SpecParams;

namespace Core.Specification.Products
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams) : base(x =>
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
        }
    }
}