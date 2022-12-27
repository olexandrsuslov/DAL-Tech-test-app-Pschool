using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pschool.Models.Dtos;
using PschoolAPIback.DbPschoolContext;
using PschoolAPIback.Extensions;
using PschoolAPIback.Models;

namespace PschoolAPIback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly PschoolContext _context;

        public ParentController(PschoolContext context)
        {
            _context = context;
        }

        // GET: api/Parent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentDto>>> GetParents()
        {
            if (_context.Parents == null)
          {
              return NotFound();
          }
          var parents = await _context.Parents.ToListAsync();
          return Ok(parents.ConvertToDto());
        }

        // GET: api/Parent/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ParentDto>> GetParent(int id)
        {
          if (_context.Parents == null)
          {
              return NotFound();
          }
            var parent = await _context.Parents.FindAsync(id);

            if (parent == null)
            {
                return NotFound();
            }

            return Ok(parent.ConvertToDtoSingle());
        }

        // PUT: api/Parent/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutParent(int id, ParentDto parentDto)
        {
            if (id != parentDto.ParentId)
            {
                return BadRequest();
            }

            var parent = parentDto.CovertToParentSingle(); 

            _context.Entry(parent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!ParentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Parent
        [HttpPost]
        public async Task<ActionResult<ParentDto>> PostItem([FromBody]ParentDto parentdto)
        {
            try
            {
                if (_context.Parents == null)
                {
                    return Problem("Entity set 'PschoolContext.Parents'  is null.");
                }

                var parent = parentdto.CovertToParentSingle();
                _context.Parents.Add(parent);
                await _context.SaveChangesAsync();
                
                 parent = await _context.Parents.FindAsync(parent.ParentId);
                 if (parent == null)
                 {
                     throw new Exception("Something went wrong when attempting to retrieve parent");
                 }

                 parentdto = parent.ConvertToDtoSingle();

                return CreatedAtAction("GetParent", new { id = parentdto.ParentId }, parentdto);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        // DELETE: api/Parent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            if (_context.Parents == null)
            {
                return NotFound();
            }
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ParentExists(int id)
        {
            return (_context.Parents?.Any(e => e.ParentId == id)).GetValueOrDefault();
        }
    }
}
