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
    public class Participant_javaController : ControllerBase
    {
        private readonly QuizDbcontext _context;

        public Participant_javaController(QuizDbcontext context)
        {
            _context = context;
        }

        // GET: api/Participant_java
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participant_java>>> GetParticipant_java()
        {
            return await _context.Participant_java.ToListAsync();
        }

        // GET: api/Participant_java/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participant_java>> GetParticipant_java(int id)
        {
            var participant_java = await _context.Participant_java.FindAsync(id);

            if (participant_java == null)
            {
                return NotFound();
            }

            return participant_java;
        }

        // PUT: api/Participant_java/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipant_java(int id, Participant_java participant_java)
        {
            if (id != participant_java.ParticipantId)
            {
                return BadRequest();
            }

            _context.Entry(participant_java).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Participant_javaExists(id))
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

        // POST: api/Participant_java
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participant_java>> PostParticipant_java(Participant_java participant_java)
        {
            _context.Participant_java.Add(participant_java);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipant_java", new { id = participant_java.ParticipantId }, participant_java);
        }

        // DELETE: api/Participant_java/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant_java(int id)
        {
            var participant_java = await _context.Participant_java.FindAsync(id);
            if (participant_java == null)
            {
                return NotFound();
            }

            _context.Participant_java.Remove(participant_java);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Participant_javaExists(int id)
        {
            return _context.Participant_java.Any(e => e.ParticipantId == id);
        }
    }
}
