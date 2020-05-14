using Core.Entities;
using Core.Specification.Products.SpecParams;

namespace Core.Specification.Products
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams): base(x =>
            (!productParams.BrandId.HasValue || x.ProductBrandId.Equals(productParams.BrandId)) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId.Equals(productParams.TypeId) && x.IsCanceled.Equals(false))
        )

        {
            
        }
    }
}