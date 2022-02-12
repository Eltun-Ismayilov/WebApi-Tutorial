﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Services;
using WebApi.Data.ViewModul;
using WebApi.Exceptions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        readonly PublisherService service;
        public PublisherController(PublisherService service)
        {
            this.service = service;
        }

        [HttpPost("Create")]
        public IActionResult AddPublis([FromBody] PublisherVM publisher)
        {
            try
            {
               var data =  service.AddPublis(publisher);

                return Created(nameof(AddPublis), data);
            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message},Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
   
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var data= service.Details(id);

            return Ok(data);

        }

        [HttpDelete("id")]

        public IActionResult Delete(int id)
        {

            try
            {
                service.delete(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



    }
}