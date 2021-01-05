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
using Core.Specification.Clients;
using Core.Specification.Clients;
using Core.Specification.Clients.SpecParams;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
 
    public class ClientsController : BaseApiController
    {
        private readonly IGenericRepository<Client> _clientRepo;
        private readonly IGenericRepository<Event> _eventRepo;
        private readonly IGenericRepository<Ticket> _voucherRepo;
        private readonly IMapper _mapper;


        public ClientsController(IGenericRepository<Client> clientRepo,
            IGenericRepository<Event> eventRepo, IGenericRepository<Ticket> voucherRepo,
            IMapper mapper)
        {
            _clientRepo = clientRepo;
            _eventRepo = eventRepo;
            _voucherRepo = voucherRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ClientToReturnDto>>> GetClients(
            [FromQuery] ClientSpecParams ClientParams)
        {
            var spec = new ClientsSpecification(ClientParams);

            var Clients = await _clientRepo.ListAsync(spec);
            var ClientDtoList = _mapper.Map<IReadOnlyList<ClientToReturnDto>>(Clients.ToList());
            
        return Ok(ClientDtoList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientToReturnDto>> GetClient([FromQuery] ClientSpecParams ClientParams)
        {
            var spec = new ClientsSpecification(ClientParams);
            
            var Client = await _clientRepo.GetEntityWithSpec(spec);

            if (Client == null) return NotFound(new ApiResponse(400));
            
            var ClientDto = _mapper.Map<ClientToReturnDto>(Client);
            return Ok(ClientDto);
        }

       
    }
}