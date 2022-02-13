using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Models;
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
        public IActionResult Details(int id)
        {
            //throw new Exception("Salam");

            var author = service.Details(id);

            return Ok(author);
        }


        [HttpDelete("id")]

        public async Task<IActionResult> Delete(int id)
        {

            try
            {
              await  service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpGet]

        public List<Author> Get()
        {
           var data=service.Get();

            return data;

        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorVM author)
        {
            var update = await service.Update(id, author);
            return Ok(update);
        }


    }
}
