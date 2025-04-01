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
    public class Participant_cController : ControllerBase
    {
        private readonly QuizDbcontext _context;

        public Participant_cController(QuizDbcontext context)
        {
            _context = context;
        }

        // GET: api/Participant_c
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participant_c>>> GetParticipant_c()
        {
            return await _context.Participant_c.ToListAsync();
        }

        // GET: api/Participant_c/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participant_c>> GetParticipant_c(int id)
        {
            var participant_c = await _context.Participant_c.FindAsync(id);

            if (participant_c == null)
            {
                return NotFound();
            }

            return participant_c;
        }

        // PUT: api/Participant_c/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // PUT: api/Participant_c/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipant_c(int id, ParticipantRestult_c _participantRestult_c)
        {
            if (id != _participantRestult_c.ParticipantId)
            {
                return BadRequest(new { message = "Participant ID mismatch" });
            }

            Participant_c? Participant_c = await _context.Participant_c.FindAsync(id);
            if (Participant_c == null)
            {
                return NotFound();
            }

            Participant_c.Score = _participantRestult_c.Score;
            Participant_c.TimeTaken = _participantRestult_c.TimeTaken;
            Participant_c.SubjectId = _participantRestult_c.SubjectId;

            _context.Entry(Participant_c).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Participant_cExists(id))
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


        // POST: api/Participant_c
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participant_c>> PostParticipant_c(Participant_c participant_c)
        {
            var temp = _context.Participant_c
                .Where(x => x.Email == participant_c.Email)
                .FirstOrDefault();

            if (temp == null)
            {
                _context.Participant_c.Add(participant_c);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetParticipant_c), new { id = participant_c.ParticipantId }, participant_c);
            }

            return Ok(temp);
        }



        // DELETE: api/Participant_c/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant_c(int id)
        {
            var participant_c = await _context.Participant_c.FindAsync(id);
            if (participant_c == null)
            {
                return NotFound();
            }

            _context.Participant_c.Remove(participant_c);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Participant_cExists(int id)
        {
            return _context.Participant_c.Any(e => e.ParticipantId == id);
        }
    }
}
