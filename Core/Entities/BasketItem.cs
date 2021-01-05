using System.Security.Principal;

namespace Core.Entities
{
    public class BasketItem : BaseEntity
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PrictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}