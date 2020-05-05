using Core.Entities;
using Core.Specification.Tickets.SpecParams;

namespace Core.Specification.Tickets
{
    public class TicketsWithEventsSpecification: BaseSpecification<Ticket>
    {
        public TicketsWithEventsSpecification(TicketSpecParams voucherSpecParams)
        : base(x =>
            x.Event.CodePublish.Equals(voucherSpecParams.CodePublish)
            )
        {
            AddInclude(x => x.Event);
            AddOrderby(x => x.Description);
            ApplyPaging(voucherSpecParams.PageSize * (voucherSpecParams.PageIndex - 1), voucherSpecParams.PageSize);

            if (!string.IsNullOrWhiteSpace(voucherSpecParams.Sort))
            {
                switch (voucherSpecParams.Sort)
                {
                    case "priceAsc" :
                        AddOrderby(p => p.Price);
                        break;
                    case "priceDesc" :
                        AddOrderbyByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderby(p => p.Description);
                        break;
                        
                }

            };
        }

    }
}