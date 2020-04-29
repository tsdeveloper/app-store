﻿using System;
using System.Linq;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class BuggyController : BaseApiController
    {
        private readonly AcessoIngressoContext _context;

        public BuggyController(AcessoIngressoContext context)
        {
            _context = context;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(Guid.NewGuid());

            if (thing == null)
                return NotFound(new ApiResponse(404));
            
            return Ok();
        }
        
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _context.Products.Find(Guid.NewGuid());

            
            return Ok(thing.ToString());
        }
        
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            var thing = _context.Products.Find(Guid.NewGuid());

            if (thing == null)
                return NotFound(new ApiResponse(400));

            return Ok();
        }
        
        /*[HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            var thing = _context.Products.Find(Guid.NewGuid());

            if (thing == null)
                return NotFound(new ApiResponse(400));
            
            return Ok();
        }*/
        
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id) => Ok();
         
        [HttpGet("modelerror")]
        public ActionResult GetModelError()
        {
            ModelState.AddModelError("yourItemFieldHere", "Your validation type here");

            return Ok(ModelState.Values.Select( x => x.Errors));
        }
    }
}