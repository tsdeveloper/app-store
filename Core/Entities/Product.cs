using System;
namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
    }
}