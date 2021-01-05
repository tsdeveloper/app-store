using System.Collections.Generic;

namespace Core.Entities
{
    public class CustomerBasket : BaseEntity
    {

        public CustomerBasket()
        {
            
        }
        public CustomerBasket(string customerBasketId)
        {
            CustomerBasketId = customerBasketId;
            
        }

        public string CustomerBasketId { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}