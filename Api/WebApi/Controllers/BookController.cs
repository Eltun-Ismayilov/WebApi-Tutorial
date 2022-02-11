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
    public class BookController : ControllerBase
    {
        readonly BooksService service;
        public BookController(BooksService service)
        {
            this.service = service;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var allbook = await service.GetAllBook();
            return Ok(allbook);
        }
        [HttpGet("Id")]
        public async Task<IActionResult> Details(int id)
        {
            return Ok(await service.GetBookId(id));

        }
        [HttpPost("Create")]
        public IActionResult Create([FromBody] BookVM book)
        {
            service.AddBook(book);
            
            return Ok();
        }
        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, [FromBody] BookVM book)
        {
            var update = await service.Update(id, book);
            return Ok(update);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.Delete(id);

            return Ok();

        }
    }
}
