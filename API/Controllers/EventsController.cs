using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Core.Specification.Events;
using Core.Specification.Events;
using Core.Specification.Events.SpecParams;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
 
    public class EventsController : BaseApiController
    {
        private readonly IGenericRepository<Event> _eventRepo;
        private readonly IGenericRepository<Ticket> _voucherRepo;
        private readonly IMapper _mapper;


        public EventsController(IGenericRepository<Event> eventRepo, IGenericRepository<Ticket> voucherRepo,
            IMapper mapper)
        {
            _eventRepo = eventRepo;
            _eventRepo = eventRepo;
            _voucherRepo = voucherRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<EventToReturnDto>>> GetEvents(
            [FromQuery] EventSpecParams eventSpecParams)
        {
            var spec = new EventsWithClientsSpecification(eventSpecParams);

            var events = await _eventRepo.ListAsync(spec);
            var eventDtoList = _mapper.Map<IReadOnlyList<EventToReturnDto>>(events.ToList());
            
        return Ok(eventDtoList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventToReturnDto>> GetEvent([FromQuery] EventSpecParams eventSpecParams)
        {
            var spec = new EventsWithClientsSpecification(eventSpecParams);
            
            var eventClient = await _eventRepo.GetEntityWithSpec(spec);

            if (eventClient == null) return NotFound(new ApiResponse(400));
            
            var eventDto = _mapper.Map<EventToReturnDto>(eventClient);
            return Ok(eventDto);
        }

       
    }
}