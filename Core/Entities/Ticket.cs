using System;

namespace Core.Entities
{
    public class Ticket : BaseEntity
    {
        public string Description { get; set; }
        public string NameDisplayUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}