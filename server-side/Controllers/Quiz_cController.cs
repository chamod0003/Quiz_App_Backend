using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using server_side.Models;

namespace server_side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Quiz_cController : ControllerBase
    {
        private readonly QuizDbcontext _context;

        public Quiz_cController(QuizDbcontext context)
        {
            _context = context;
        }

        // GET: api/Quiz_c
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz_c>>> GetQuiz_c()
        {
            var random6Q = await _context.Quiz_c
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

        // GET: api/Quiz_c/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz_c>> GetQuiz_c(int id)
        {
            var quiz_c = await _context.Quiz_c.FindAsync(id);

            if (quiz_c == null)
            {
                return NotFound();
            }

            return quiz_c;
        }

        // PUT: api/Quiz_c/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz_c(int id, Quiz_c quiz_c)
        {
            if (id != quiz_c.QnId)
            {
                return BadRequest();
            }

            _context.Entry(quiz_c).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Quiz_cExists(id))
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

        // POST: api/Quiz_c
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("GetAnswers_c")]
        public async Task<ActionResult<Quiz_c>> PostQuiz_c(int[] qnIds)
        {
            var answers = await (_context.Quiz_c
                .Where(x => qnIds.Contains(x.QnId))
                .Select(y => new
                {
                    y.QnId,
                    y.QnInWords,
                    y.ImageName,
                    Options = new string[] { y.Option1, y.Option2, y.Option3, y.Option4 },
                    y.Answer
                })).ToListAsync();
            return Ok(answers);
        }
        // DELETE: api/Quiz_c/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz_c(int id)
        {
            var quiz_c = await _context.Quiz_c.FindAsync(id);
            if (quiz_c == null)
            {
                return NotFound();
            }

            _context.Quiz_c.Remove(quiz_c);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Quiz_cExists(int id)
        {
            return _context.Quiz_c.Any(e => e.QnId == id);
        }
    }
}
