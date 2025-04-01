using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_side.Models;

namespace server_side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Quiz_javaController : ControllerBase
    {
        private readonly QuizDbcontext _context;

        public Quiz_javaController(QuizDbcontext context)
        {
            _context = context;
        }

        // GET: api/Quiz_java
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz_java>>> GetQuiz_java()
        {
            return await _context.Quiz_java.ToListAsync();
        }

        // GET: api/Quiz_java/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz_java>> GetQuiz_java(int id)
        {
            var quiz_java = await _context.Quiz_java.FindAsync(id);

            if (quiz_java == null)
            {
                return NotFound();
            }

            return quiz_java;
        }

        // PUT: api/Quiz_java/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz_java(int id, Quiz_java quiz_java)
        {
            if (id != quiz_java.QnId)
            {
                return BadRequest();
            }

            _context.Entry(quiz_java).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Quiz_javaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Quiz_java
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quiz_java>> PostQuiz_java(Quiz_java quiz_java)
        {
            _context.Quiz_java.Add(quiz_java);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz_java", new { id = quiz_java.QnId }, quiz_java);
        }

        // DELETE: api/Quiz_java/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz_java(int id)
        {
            var quiz_java = await _context.Quiz_java.FindAsync(id);
            if (quiz_java == null)
            {
                return NotFound();
            }

            _context.Quiz_java.Remove(quiz_java);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Quiz_javaExists(int id)
        {
            return _context.Quiz_java.Any(e => e.QnId == id);
        }
    }
}
