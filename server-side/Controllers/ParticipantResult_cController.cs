using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_side.Models;

namespace server_side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantResult_cController : ControllerBase
    {
        private readonly QuizDbcontext _context;

        public ParticipantResult_cController(QuizDbcontext context)
        {
            _context = context;
        }

        // GET: api/ParticipantResult_c
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllResults()
        {
            var results = await _context.ParticipantResult_c
                .Include(r => r.Subject)
                .OrderByDescending(r => r.AttemptedAt)
                .Select(r => new
                {
                    r.Id,
                    r.ParticipantId,
                    r.SubjectId,
                    r.Subject.SubjectName,
                    r.Score,
                    r.TimeTaken,
                    r.AttemptedAt
                })
                .ToListAsync();

            return Ok(results);
        }

        // GET: api/ParticipantResult_c/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipantResult_c>> GetResultById(int id)
        {
            var result = await _context.ParticipantResult_c.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // ✅ UPDATED: GET: api/ParticipantResult_c/participant/{participantId}
        [HttpGet("participant/{participantId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetResultsByParticipant(int participantId)
        {
            var results = await _context.ParticipantResult_c
                .Where(r => r.ParticipantId == participantId)
                .Include(r => r.Subject)
                .OrderByDescending(r => r.AttemptedAt)
                .Select(r => new
                {
                    r.Id,
                    r.ParticipantId,
                    r.SubjectId,
                    r.Subject.SubjectName,
                    r.Score,
                    r.TimeTaken,
                    r.AttemptedAt
                })
                .ToListAsync();

            return Ok(results);
        }

        // POST: api/ParticipantResult_c
        [HttpPost]
        public async Task<ActionResult<ParticipantResult_c>> PostResult(ParticipantResult_c result)
        {
            result.AttemptedAt = DateTime.UtcNow;
            _context.ParticipantResult_c.Add(result);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResultById), new { id = result.Id }, result);
        }

        // DELETE: api/ParticipantResult_c/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _context.ParticipantResult_c.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.ParticipantResult_c.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResultExists(int id)
        {
            return _context.ParticipantResult_c.Any(e => e.Id == id);
        }
    }
}
