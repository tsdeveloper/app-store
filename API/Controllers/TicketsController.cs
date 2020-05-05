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
using Core.Specification.Tickets;
using Core.Specification.Tickets.SpecParams;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
 
    public class TicketsController : BaseApiController
    {
        private readonly IGenericRepository<Event> _eventRepo;
        private readonly IGenericRepository<Ticket> _tickerRepo;
        private readonly IMapper _mapper;


        public TicketsController(IGenericRepository<Ticket> tickerRepo, IGenericRepository<Event> eventRepo,
            IMapper mapper)
        {
            _tickerRepo = tickerRepo;
            _eventRepo = eventRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TicketToReturnDto>>> GetVouchers(
            [FromQuery] TicketSpecParams tickerSpecParams)
        {
            var spec = new TicketsWithEventsSpecification(tickerSpecParams);

            var tickers = await _tickerRepo.ListAsync(spec);
            var tickerDtoList = _mapper.Map<IReadOnlyList<TicketToReturnDto>>(tickers.ToList());
            
        return Ok(tickerDtoList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TicketToReturnDto>> GetVoucher([FromQuery] TicketSpecParams tickerSpecParams)
        {
            var spec = new TicketsWithEventsSpecification(tickerSpecParams);
            
            var tickerClient = await _tickerRepo.GetEntityWithSpec(spec);

            if (tickerClient == null) return NotFound(new ApiResponse(400));
            
            var tickerDto = _mapper.Map<TicketToReturnDto>(tickerClient);
            return Ok(tickerDto);
        }

       
    }
}