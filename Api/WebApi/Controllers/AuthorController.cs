using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Services;
using WebApi.Data.ViewModul;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        readonly AuthorsService service;
        public AuthorController(AuthorsService service)
        {
            this.service = service;
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] AuthorVM author)
        {
            service.AddBook(author);

            return Ok();
        }

        [HttpGet("id")]
        public IActionResult Create(int id)
        {
          var author= service.Details(id);

            return Ok(author);
        }



    }
}
