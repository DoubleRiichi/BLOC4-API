using Microsoft.AspNetCore.Mvc;
using BLOC4_API;
using BLOC4_API.Models;
using System.Text.Json;

namespace BLOC4_API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ServicesController : ControllerBase
    {

        private readonly BLOC4db _context;

        public ServicesController(BLOC4db context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get")]
        public ActionResult Get()
        {
            var rows = _context.Services.ToList();

            if(rows.Any())
            {
                return Ok(rows);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("find/{id}")]
        public ActionResult Find(int id)
        {
            var row = _context.Services.Find(id);

            if (row != null)
            {
                return Ok(row);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Services service)
        {
            // since id is autoincrement, we shouldn't accept a post request with an user given id
            if (service == null || service.Id != null)
            {
                return BadRequest();
            }

            try
            {
                _context.Services.Add(service);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Create), new { id = service.Id }, service);
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }

        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] Services service)
        {
            if (service == null || service.Id == null)
            {
                return BadRequest();
            }

            var existingService = _context.Services.Find(service.Id);
            if (existingService == null)
            {
                return NotFound();
            }

            existingService.Nom = service.Nom;
        

            try
            {
                _context.Services.Update(existingService);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500); // 500 = internal server error 
            }

        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            try
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500); // 500 = internal server error 
            }

        }
    }
}
