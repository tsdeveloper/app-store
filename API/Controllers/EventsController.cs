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
using Core.Specification.Tickets;
using Core.Specification.Tickets.SpecParams;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
 
    public class EventsController : BaseApiController
    {
        private readonly IGenericRepository<Event> _eventRepo;
        private readonly IGenericRepository<Ticket> _tickerRepo;
        private readonly IMapper _mapper;


        public EventsController(IGenericRepository<Event> eventRepo, IGenericRepository<Ticket> tickerRepo,
            IMapper mapper)
        {
            _eventRepo = eventRepo;
            _eventRepo = eventRepo;
            _tickerRepo = tickerRepo;
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

        [HttpGet]
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
        
        [HttpGet]
        [Route("evento-cliente/{codePublish}/ingressos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TicketToReturnDto>> GetTicketByEventId([FromRoute] TicketSpecParams ticketSpecParams)
        {
       
            var spec = new TicketsWithEventsSpecification(ticketSpecParams);
            
            var ticketClient = await _tickerRepo.ListAsync(spec);

            var ticketToReturn = _mapper.Map<IReadOnlyList<TicketToReturnDto>>(ticketClient);
            return Ok(ticketToReturn);
        }


       
    }
}