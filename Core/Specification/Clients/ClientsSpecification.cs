using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specification.Clients.SpecParams;

namespace Core.Specification.Clients
{
    public class ClientsSpecification: BaseSpecification<Client>
    {
        public ClientsSpecification(ClientSpecParams clientSpecParams)
        
        {
       
            AddOrderby(x => x.Name);
            ApplyPaging(clientSpecParams.PageSize * (clientSpecParams.PageIndex - 1), clientSpecParams.PageSize);

            if (!string.IsNullOrWhiteSpace(clientSpecParams.Sort))
            {
                switch (clientSpecParams.Sort)
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