using Core.Entities;

namespace Core.Specification.Products.SpecParams
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams): base(x =>
            (!productParams.BrandId.HasValue || x.ProductBrandId.Equals(productParams.BrandId)) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId.Equals(productParams.TypeId))
        )

        {
            
        }
    }
}