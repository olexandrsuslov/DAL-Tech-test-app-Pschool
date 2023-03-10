using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pschool.Models.Dtos;
using Pschool.Models.RequestFeatures;
using PschoolAPIback.DbPschoolContext;
using PschoolAPIback.Extensions;
using PschoolAPIback.Models;
using PschoolAPIback.Paging;

namespace PschoolAPIback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly PschoolContext _context;

        public StudentController(PschoolContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult> GetStudents([FromQuery] DisplayParameters displayParameters)
        { 
            if (_context.Students == null)
            {
                return NotFound();
            }

            
            var students = await _context.Students.Search(displayParameters.SearchTerm).Sort(displayParameters.OrderBy).ToListAsync();
            if (displayParameters.SearchParent != null)
            {
                students = students.Where(x => x.ParentId == Int32.Parse(displayParameters.SearchParent)).ToList();
            }
            var studentlistdto= PagedList<StudentDto>
                .ToPagedList(students.ConvertToDtoStudents(), displayParameters.PageNumber, displayParameters.PageSize);
          
            Response.Headers.Add("Y-Paging", JsonConvert.SerializeObject(studentlistdto.MetaData));
            return Ok(studentlistdto);
        }

        // GET: api/Student/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
          if (_context.Students == null)
          {
              return NotFound();
          }
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student.ConvertToStudentDtoSingle());
        }

        // PUT: api/Student/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutStudent(int id, StudentDto studentDto)
        {
            if (id != studentDto.Id)
            {
                return BadRequest();
            }

            var student = studentDto.CovertToStudentSingle(); 

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!StudentExists(id))
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

        // POST: api/Student
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<StudentDto>> PostItem([FromBody]StudentDto studentdto)
        {
            try
            {
                if (_context.Students == null)
                {
                    return Problem("Entity set 'PschoolContext.Students'  is null.");
                }

                var student = studentdto.CovertToStudentSingle();
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                
                student = await _context.Students.FindAsync(student.Id);
                 if (student == null)
                 {
                     throw new Exception("Something went wrong when attempting to retrieve student");
                 }

                 studentdto = student.ConvertToStudentDtoSingle();

                return CreatedAtAction("GetStudent", new { id = studentdto.Id }, studentdto);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
