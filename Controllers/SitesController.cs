﻿using Microsoft.AspNetCore.Mvc;
using BLOC4_API;
using BLOC4_API.Models;
using System.Text.Json;

namespace BLOC4_API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SitesController : ControllerBase
    {

        private readonly BLOC4db _context;

        public SitesController(BLOC4db context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get")]
        public ActionResult Get()
        {
            var rows = _context.Sites.ToList();

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
            var row = _context.Sites.Find(id);

            if (row != null)
            {
                return Ok(row);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Sites site)
        {
            // since id is autoincrement, we shouldn't accept a post request with an user given id
            if (site == null || site.Id != null)
            {
                return BadRequest();
            }

            try
            {
                _context.Sites.Add(site);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Create), new { id = site.Id }, site);
            }
            catch (Exception ex)
            {

                return StatusCode(500);
            }

        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] Sites site)
        {
            if (site == null || site.Id == null)
            {
                return BadRequest();
            }

            var existingSite = _context.Sites.Find(site.Id);
            if (existingSite == null)
            {
                return NotFound();
            }

            existingSite.Nom = site.Nom;


            try
            {
                _context.Sites.Update(existingSite);
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
            var site = _context.Sites.Find(id);
            if (site == null)
            {
                return NotFound();
            }

            try
            {
                _context.Sites.Remove(site);
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