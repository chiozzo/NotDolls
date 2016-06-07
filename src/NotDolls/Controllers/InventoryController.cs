using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using NotDolls.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotDolls.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  [EnableCors("AllowDevEnvironment")]
  public class InventoryController : Controller
  {

    private NotDollsContext _context;

    public InventoryController(NotDollsContext context)
    {
      _context = context;
    }

    // GET: api/values
    [HttpGet]
    public IActionResult GetInventoryByUser(int? InventoryId, int? GeekId, int? Year, string Name)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IQueryable<Inventory> inventory = _context.Inventory;

      if (InventoryId != null)
      {
        inventory = inventory.Where(item => item.InventoryId == InventoryId);
      }

      if (GeekId != null)
      {
        inventory = inventory.Where(item => item.GeekId == GeekId);
      }

      if (Year != null)
      {
        inventory = inventory.Where(item => item.Year == Year);
      }

      if (Name != null)
      {
        inventory = inventory.Where(item => item.Name == Name);
      }

      if (inventory == null)
      {
        return NotFound();
      }
      return Ok(inventory);
    }

    // POST api/values
    [HttpPost]
    public IActionResult Post([FromBody]Inventory newInventoryItem)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      _context.Inventory.Add(newInventoryItem);
      try
      {
        _context.SaveChanges();
      }
      catch (DbUpdateException)
      {
        if (InventoryExists(newInventoryItem.InventoryId))
        {
          return new StatusCodeResult(StatusCodes.Status409Conflict);
        }
        else
        {
          return new StatusCodeResult(StatusCodes.Status418ImATeapot);
        }
      }

      return CreatedAtRoute("GetInventory", new { id = newInventoryItem.InventoryId }, newInventoryItem);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]Inventory inventoryItemToUpdate)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      Inventory inventoryItem = _context.Inventory.Single(item => item.InventoryId == id);
      inventoryItemToUpdate.InventoryId = inventoryItem.InventoryId;
      _context.Inventory.Add(inventoryItemToUpdate);
      try
      {
        _context.SaveChanges();
      }
      catch (DbUpdateException)
      {
        if (InventoryExists(inventoryItemToUpdate.InventoryId))
        {
          return new StatusCodeResult(StatusCodes.Status409Conflict);
        }
        else
        {
          return new StatusCodeResult(StatusCodes.Status418ImATeapot);
        }
      }

      return CreatedAtRoute("GetInventory", new { id = inventoryItemToUpdate.InventoryId }, inventoryItemToUpdate);
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      Inventory inventoryItemToDelete = _context.Inventory.Single(item => item.InventoryId == id);

      _context.Inventory.Remove(inventoryItemToDelete);
      _context.SaveChanges();
      return new StatusCodeResult(StatusCodes.Status200OK);
    }

    public bool InventoryExists(int id)
    {
      return _context.Inventory.Count(item => item.InventoryId == id) > 0;
    }
  }
}
