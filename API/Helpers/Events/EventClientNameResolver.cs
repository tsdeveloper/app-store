using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers.Events
{
    public class EventClientNameResolver : IValueResolver<Event, EventToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public EventClientNameResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Event source, EventToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (source.Client.Name != null && string.IsNullOrWhiteSpace(destination.ClientName))
                return source.Client.Name;
            
            return null;
        }
    }
}