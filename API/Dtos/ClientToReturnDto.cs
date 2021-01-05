using System;
using Core.Entities;

namespace API.Dtos
{
    public class ClientToReturnDto : BaseEntity
    {
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Address { get; set; }
        
    }
}