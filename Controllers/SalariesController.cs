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

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] SalariesRequest salarieRequest)
        {
            var auth = _context.Connexion.Where(x => x.Token == salarieRequest.token).FirstOrDefault();

            if (auth == null) {
                return Unauthorized();
            }                        
            // since id is autoincrement, we shouldn't accept a post request with an user given id
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
