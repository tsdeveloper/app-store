using System;
namespace Core.Entities
{
    public class Event : BaseEntity
    {
        public string CodePublish { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PublishUrl { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}