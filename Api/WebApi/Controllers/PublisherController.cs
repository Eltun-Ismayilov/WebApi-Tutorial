using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly ILogger<PublisherController> logger;
        public PublisherController(PublisherService service, ILogger<PublisherController> logger)
        {
            this.service = service;
            this.logger = logger;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get(string sortBy,string search)
        {
            try
            {
                logger.LogInformation("This is just a log in GetAllPubLisher()");
                var publishers = await service.Get(sortBy, search);
                return Ok(publishers);
            }
            catch (Exception)
            {

                return BadRequest("Sorry,we could not load the publishers");
            }
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var data= service.Details(id);
           // throw new Exception("Salam");

            return Ok(data);

        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {

            try
            {
                service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, [FromBody] PublisherVM publisher)
        {
            var update = await service.Update(id, publisher);
            return Ok(update);
        }

    }
}
