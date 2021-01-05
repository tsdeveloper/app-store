using System;
using Core.Entities;

namespace API.Dtos
{
    public class TicketToReturnDto : BaseEntity
    {
        public string Description { get; set; }
        public string NameDisplayUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string EventName { get; set; }
        
    }
}