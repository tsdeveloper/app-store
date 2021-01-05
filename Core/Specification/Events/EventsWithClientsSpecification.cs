using Core.Entities;
using Core.Specification.Events.SpecParams;
using Core.Specification.Products.SpecParams;

namespace Core.Specification.Events
{
    public class EventsWithClientsSpecification: BaseSpecification<Event>
    {
        public EventsWithClientsSpecification(EventSpecParams eventSpecParams)
        : base(x =>
                !eventSpecParams.ClientId.HasValue || x.ClientId.Equals(eventSpecParams.ClientId)
            )
        {
            AddInclude(x => x.Client);
            AddOrderby(x => x.Name);
            ApplyPaging(eventSpecParams.PageSize * (eventSpecParams.PageIndex - 1), eventSpecParams.PageSize);

            if (!string.IsNullOrWhiteSpace(eventSpecParams.Sort))
            {
                switch (eventSpecParams.Sort)
                {
                  
                    case "nameDesc" :
                        AddOrderbyByDescending(p => p.Name);
                        break;
                    default:
                        AddOrderby(p => p.Name);
                        break;
                        
                }

            };
        }

        
    }
}