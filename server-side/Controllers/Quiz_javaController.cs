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
            var random6Q = await _context.Quiz_java
                .Select(x => new
                {
                    x.QnId,
                    x.QnInWords,
                    x.ImageName,
                    Option = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                })
                .OrderBy(x => Guid.NewGuid())  // Randomize order
                .Take(6)  // Take only 6 questions
                .ToListAsync();

            return Ok(random6Q);  // Return the selected questions
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
        [HttpPost]
        [Route("GetAnswers_java")]
        public async Task<ActionResult<Quiz_java>> GetAnswers_java([FromBody] int[] qnIds)
        {
            var answers = await _context.Quiz_java
                .Where(x => qnIds.Contains(x.QnId))
                .Select(y => new
                {
                    y.QnId,
                    y.QnInWords,
                    y.ImageName,
                    Options = new string[] { y.Option1, y.Option2, y.Option3, y.Option4 },
                    y.Answer
                }).ToListAsync();

            return Ok(answers);
        }

        // POST: api/Quiz_java (Create new quiz question)
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
