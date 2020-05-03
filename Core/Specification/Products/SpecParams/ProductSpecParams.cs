using System;
using Core.Entities;

namespace Core.Specification.Products.SpecParams
{
    public class ProductSpecParams : BaseEntity
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } =  1;
        private int _pageSize { get; set; } = 10;
    
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public Guid? BrandId { get; set; }
        public Guid? TypeId { get; set; }
        public string Sort { get; set; }
    }
}