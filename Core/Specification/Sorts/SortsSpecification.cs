using Core.Entities;
using Core.Specification.Sorts.SpecParams;

namespace Core.Specification.Sorts
{
    public class SortsSpecification: BaseSpecification<SortOption>
    {
        public SortsSpecification(SortSpecParams sortSpecParams)
        : base(x =>
        x.IsCanceled.Equals(false)            
        )
        {
       
            AddOrderby(x => x.Name);
            ApplyPaging(sortSpecParams.PageSize * (sortSpecParams.PageIndex - 1), sortSpecParams.PageSize);

            if (!string.IsNullOrWhiteSpace(sortSpecParams.Sort))
            {
                switch (sortSpecParams.Sort)
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