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

        // Récupère l'ensemble des Services du Site lié
        // @Return: 200 Ok et Liste Salariés
        // @Return: 404 NotFound
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

        // Récupère un des Services par son ID et le Site qui lui est associé
        // @Return: 200 Ok et un objet Services
        // @Return: 404 NotFound
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

        // Créer un nouveau Service dans la base de donnée suivant un objet Service passé dans la requête avec un Token valide (voir ServicesRequest)
        // @Return Unauthorized si token invalide 
        // @Return BadRequest
        // @Return InternalServerError on exception
        // @Return 200 Ok et le nouveau salarié
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

        // Modifie un Service dans la base de donnée suivant un objet Service passé dans la requête avec un Token valide (voir ServiceRequest)
        // @Return Unauthorized si token invalide 
        // @Return BadRequest
        // @Return InternalServerError on exception
        // @Return 200 Ok et le service modifié
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

        // Supprime un Service dans la base de donnée suivant un id passé dans la requête avec un Token valide (voir ServiceRequest)
        // @Return Unauthorized si token invalide 
        // @Return BadRequest
        // @Return InternalServerError on exception
        // @Return 200 Ok
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
