using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotDolls.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotDolls.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  [EnableCors("AllowDevEnvironment")]
  public class GeekController : Controller
  {
    private NotDollsContext _context;

    public GeekController(NotDollsContext context)
    {
      _context = context;
    }


    // GET: api/values
    [HttpGet(Name = "GetAllGeeks")]
    public IActionResult Get([FromQuery]string username)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      // could also provide a sub-query for Figurines to return actual results instead of a link to another GET call
      IQueryable<Geek> geeks = from user in _context.Geek
                               select new Geek
                               {
                                 GeekId = user.GeekId,
                                 UserName = user.UserName,
                                 EmailAddress = user.EmailAddress,
                                 CreatedDate = user.CreatedDate,
                                 Location = user.Location,
                                 FigurineHREF = String.Format("api/Inventory/?GeekId=" + user.GeekId)
                               };

      if (username != null)
      {
        geeks = geeks.Where(g => g.UserName == username);
      }

      if (geeks == null)
      {
        return NotFound();
      }

      return Ok(geeks);
    }

    // GET api/values/5
    [HttpGet("{id}", Name = "GetGeek")]
    public IActionResult Get(int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      Geek geek = _context.Geek.Single(m => m.GeekId == id);

      if (geek == null)
      {
        return NotFound();
      }

      return Ok(geek);
    }

    // POST api/values
    [HttpPost]
    public IActionResult Post([FromBody]Geek newGeek)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IQueryable<Geek> existingUser = from geek in _context.Geek
                                      where geek.UserName == newGeek.UserName
                                      select geek;

      int userExists = existingUser.Count<Geek>();
      if (userExists > 0)
      {
        return new StatusCodeResult(StatusCodes.Status409Conflict);
      }

      //actually modifies newGeek to give GeekId a non-null value (property already exists due to ModelState casting)
      _context.Geek.Add(newGeek);

      try
      {
        _context.SaveChanges();
      }
      catch (DbUpdateException)
      {
        if (GeekExists(newGeek.GeekId))
        {
          return new StatusCodeResult(StatusCodes.Status409Conflict);
        }
        else
        {
          return new StatusCodeResult(StatusCodes.Status418ImATeapot);
        }
      }

      return CreatedAtRoute("GetGeek", new { id = newGeek.GeekId }, newGeek);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]Geek geekToUpdate)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != geekToUpdate.GeekId)
      {
        return BadRequest();
      }

      Geek singleGeek = _context.Geek.Single(geek => geek.GeekId == id);

      geekToUpdate.GeekId = singleGeek.GeekId;

      _context.Geek.Add(geekToUpdate);

      try
      {
        _context.SaveChanges();
      }
      catch (DbUpdateException)
      {
        if (GeekExists(geekToUpdate.GeekId))
        {
          return new StatusCodeResult(StatusCodes.Status409Conflict);
        }
        else
        {
          return new StatusCodeResult(StatusCodes.Status418ImATeapot);
        }
        throw;
      }
      return CreatedAtRoute("GetGeek", new { id = geekToUpdate.GeekId }, geekToUpdate);
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      Geek geekToDelete = _context.Geek.Single(geek => geek.GeekId == id);

      if (geekToDelete == null)
      {
        return NotFound();
      }

      _context.Geek.Remove(geekToDelete);
      _context.SaveChanges();

      return Ok(geekToDelete);
    }

    private bool GeekExists(int id)
    {
      return _context.Geek.Count(geek => geek.GeekId == id) > 0;
    }
  }
}
