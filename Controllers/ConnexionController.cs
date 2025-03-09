using Microsoft.AspNetCore.Mvc;
using BLOC4_API;
using BLOC4_API.Models;
using System.Text.Json;
using System.Security.Cryptography;

namespace BLOC4_API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ConnexionController : ControllerBase
    {

        private readonly BLOC4db _context;

        public ConnexionController(BLOC4db context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Connexion connexion)
        {

            var user = _context.Connexion.Where(x => x.Id == connexion.Id).FirstOrDefault();


            if (user == null) {
                return NotFound();              
            }

            var successfulLogin = BCrypt.Net.BCrypt.EnhancedVerify(connexion.Password, user.Password);

            if (!successfulLogin) {
                return NotFound();
            } 


            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);

            user.Token = apiKey;

            try
            {
                _context.Connexion.Update(user);
                _context.SaveChanges();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {
                    code = "500"
                }); // 500 = internal server error 
            }

        }


        [HttpPost]
        [Route("logout")]
        public IActionResult Logout([FromBody] Connexion connexion)
        {

            var user = _context.Connexion.Where(x => x.Token == connexion.Token).FirstOrDefault();

            if (user == null) {
                return NotFound();              
            }

            user.Token = null;

            try
            {
                _context.Connexion.Update(user);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500); // 500 = internal server error 
            }

        }


        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] string password)
        {

            try
            {
                var user = new Connexion{
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password),
                    Token = null
                }; 

                _context.Connexion.Add(user);
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
