using Microsoft.AspNetCore.Mvc;
using BLOC4_API;
using BLOC4_API.Models;
using System.Text.Json;

namespace BLOC4_API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SalariesController : ControllerBase
    {

        private readonly BLOC4db _context;

        public SalariesController(BLOC4db context)
        {
            _context = context;
        }
        

        // Récupère l'ensemble des Salariés et les objets Sites et Services liés
        // @Return: 200 Ok et Liste Salariés
        // @Return: 404 NotFound
        [HttpGet]
        [Route("get")]
        public ActionResult Get()
        {
            var rows = from salarie in _context.Salaries
                       join service in _context.Services
                       on salarie.Services_id equals service.Id into serviceGroup
                       from service in serviceGroup.DefaultIfEmpty()
                       join site in _context.Sites
                       on service.Sites_id equals site.Id into siteGroup
                       from site in siteGroup.DefaultIfEmpty()
                       select new
                       {
                           salarie.Id,
                           salarie.Nom,
                           salarie.Prenom,
                           salarie.Telephone_fixe,
                           salarie.Telephone_mobile,
                           salarie.Email,
                           service = new {
                            service.Id, 
                            service.Nom,
                            site = new {
                                site.Id,
                                site.Nom,
                            }
                           },
                       };

            if (rows.Any())
            {
                return Ok(rows);
            }

            return NotFound();
        }
        // Récupère un des Salariés par son ID et le Site et Service qui lui est associé
        // @Return: 200 Ok et un objet Salarié
        // @Return: 404 NotFound
        [HttpGet]
        [Route("find/{id}")]
        public ActionResult Find(int id)
        {
            var row = from salarie in _context.Salaries
                      where salarie.Id == id
                      join service in _context.Services
                      on salarie.Services_id equals service.Id
                      select new
                      {
                          salarie.Id,
                          salarie.Nom,
                          salarie.Prenom,
                          salarie.Telephone_fixe,
                          salarie.Telephone_mobile,
                          salarie.Email,
                          service,
                      };

            if (row != null)
            {
                return Ok(row);
            }

            return NotFound();
        }


        // Créer un nouveau Salarié dans la base de donnée suivant un objet Salarié passé dans la requête avec un Token valide (voir SalariesRequest)
        // @Return Unauthorized si token invalide 
        // @Return BadRequest
        // @Return InternalServerError on exception
        // @Return 200 Ok et le nouveau salarié
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] SalariesRequest salarieRequest)
        {
            var auth = _context.Connexion.Where(x => x.Token == salarieRequest.token).FirstOrDefault();

            if (auth == null) {
                return Unauthorized();
            }                        
           
            if (salarieRequest == null || salarieRequest.salaries.Id != null || salarieRequest.salaries.Services_id == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Salaries.Add(salarieRequest.salaries);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Create), new { id = salarieRequest.salaries.Id }, salarieRequest);
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }

        }

        // Modifie un Salarié dans la base de donnée suivant un objet Salarié passé dans la requête avec un Token valide (voir SalariesRequest)
        // @Return Unauthorized si token invalide 
        // @Return BadRequest
        // @Return InternalServerError on exception
        // @Return 200 Ok et le salarié modifié
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] SalariesRequest salarieRequest)
        {
            var auth = _context.Connexion.Where(x => x.Token == salarieRequest.token).FirstOrDefault();

            if (auth == null) {
                return Unauthorized();
            }          
                          
            if (salarieRequest == null || salarieRequest.salaries.Id == null || salarieRequest.salaries.Services_id == null)
            {
                return BadRequest();
            }

            var existingSalarie = _context.Salaries.Find(salarieRequest.salaries.Id);
            if (existingSalarie == null)
            {
                return NotFound();
            }

            existingSalarie.Prenom = salarieRequest.salaries.Prenom;
            existingSalarie.Nom = salarieRequest.salaries.Nom;
            existingSalarie.Telephone_fixe = salarieRequest.salaries.Telephone_fixe;
            existingSalarie.Telephone_mobile = salarieRequest.salaries.Telephone_mobile;
            existingSalarie.Email = salarieRequest.salaries.Email;
            existingSalarie.Services_id = salarieRequest.salaries.Services_id;


            try
            {
                _context.Salaries.Update(existingSalarie);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500); // 500 = internal server error 
            }

        }
        // Supprime un Salarié dans la base de donnée suivant un id passé dans la requête avec un Token valide (voir SalariesRequest)
        // @Return Unauthorized si token invalide 
        // @Return BadRequest
        // @Return InternalServerError on exception
        // @Return 200 Ok
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var salarie = _context.Salaries.Find(id);
            if (salarie == null)
            {
                return NotFound();
            }

            try
            {
                _context.Salaries.Remove(salarie);
                _context.SaveChanges();
                return Ok(new {error = "Ok"});
            }
            catch (Exception ex)
            {
                return StatusCode(500); // 500 = internal server error 
            }

        }
    }
}
