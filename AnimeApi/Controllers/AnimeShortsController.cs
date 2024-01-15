using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeApi.Models;

namespace AnimeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeShortsController : ControllerBase
    {
        private readonly AnimeStoreContext _context;

        public AnimeShortsController(AnimeStoreContext context)
        {
            _context = context;
        }

        // GET: api/AnimeShorts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeShorts>>> GetAnimeShorts()
        {
            return await _context.AnimeShorts.ToListAsync();
        }

        // GET: api/AnimeShorts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimeShorts>> GetAnimeShorts(long id)
        {
            var animeShorts = await _context.AnimeShorts.FindAsync(id);

            if (animeShorts == null)
            {
                return NotFound();
            }

            return animeShorts;
        }

        // PUT: api/AnimeShorts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimeShorts(long id, AnimeShorts animeShorts)
        {
            if (id != animeShorts.Id)
            {
                return BadRequest();
            }

            _context.Entry(animeShorts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimeShortsExists(id))
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

        // POST: api/AnimeShorts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnimeShorts>> PostAnimeShorts(AnimeShorts animeShorts)
        {
            _context.AnimeShorts.Add(animeShorts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimeShorts", new { id = animeShorts.Id }, animeShorts);
        }

        // DELETE: api/AnimeShorts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimeShorts(long id)
        {
            var animeShorts = await _context.AnimeShorts.FindAsync(id);
            if (animeShorts == null)
            {
                return NotFound();
            }

            _context.AnimeShorts.Remove(animeShorts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimeShortsExists(long id)
        {
            return _context.AnimeShorts.Any(e => e.Id == id);
        }
    }
}
