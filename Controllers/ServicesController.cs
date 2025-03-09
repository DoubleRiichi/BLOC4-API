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
            // var rows = _context.Services.ToList();
            var data = from services in _context.Services
                       join site in _context.Sites
                       on services.Sites_id equals site.Id
                       select new {
                        services.Id,
                        services.Nom,
                        site
                       };
                    

            if(data.Any())
            {
                return Ok(data);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("find/{id}")]
        public ActionResult Find(int id)
        {
            var data = from services in _context.Services
                       where services.Id == id
                       join site in _context.Sites
                       on services.Sites_id equals site.Id
                       select new {
                        services.Id,
                        services.Nom,
                        site
                       };
                    

            if(data.Any())
            {
                return Ok(data);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] ServicesRequest serviceRequest)
        {
            var auth = _context.Connexion.Where(x => x.Token == serviceRequest.token).FirstOrDefault();

            if (auth == null) {
                return Unauthorized();
            }            
            // since id is autoincrement, we shouldn't accept a post request with an user given id
            if (serviceRequest == null || serviceRequest.services.Id != null)
            {
                return BadRequest();
            }

            try
            {
                _context.Services.Add(serviceRequest.services);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Create), new { id = serviceRequest.services.Id }, serviceRequest.services);
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }

        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] ServicesRequest serviceRequest)
        {
            var auth = _context.Connexion.Where(x => x.Token == serviceRequest.token).FirstOrDefault();

            if (auth == null) {
                return Unauthorized();
            }            
                        
            if (serviceRequest == null || serviceRequest.services.Id == null  )
            {
                return BadRequest();
            }

            var existingService = _context.Services.Find(serviceRequest.services.Id);
            if (existingService == null)
            {
                return NotFound();
            }

            existingService.Nom = serviceRequest.services.Nom;
            existingService.Sites_id = serviceRequest.services.Sites_id;
        

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
