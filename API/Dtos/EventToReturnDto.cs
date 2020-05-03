using System;
using Core.Entities;

namespace API.Dtos
{
    public class EventToReturnDto : BaseEntity
    {
        public string CodePublish { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PublishUrl { get; set; }
        public string ClientName { get; set; }
        
        
    }
}