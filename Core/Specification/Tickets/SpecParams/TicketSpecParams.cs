using System;
using Core.Entities;

namespace Core.Specification.Tickets.SpecParams
{
    public class TicketSpecParams : BaseEntity
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } =  1;
        private int _pageSize { get; set; } = 10;
    
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string CodePublish { get; set; }
        public string Sort { get; set; }
    }
}