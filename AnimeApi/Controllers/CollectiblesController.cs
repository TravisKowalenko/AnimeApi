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
    public class CollectiblesController : ControllerBase
    {
        private readonly AnimeStoreContext _context;

        public CollectiblesController(AnimeStoreContext context)
        {
            _context = context;
        }

        // GET: api/Collectibles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collectible>>> GetCollectibles()
        {
            return await _context.Collectibles.ToListAsync();
        }

        // GET: api/Collectibles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Collectible>> GetCollectible(int id)
        {
            var collectible = await _context.Collectibles.FindAsync(id);

            if (collectible == null)
            {
                return NotFound();
            }

            return collectible;
        }

        // PUT: api/Collectibles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollectible(int id, Collectible collectible)
        {
            if (id != collectible.Id)
            {
                return BadRequest();
            }

            _context.Entry(collectible).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectibleExists(id))
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

        // POST: api/Collectibles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Collectible>> PostCollectible(Collectible collectible)
        {
            _context.Collectibles.Add(collectible);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollectible", new { id = collectible.Id }, collectible);
        }

        // DELETE: api/Collectibles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollectible(int id)
        {
            var collectible = await _context.Collectibles.FindAsync(id);
            if (collectible == null)
            {
                return NotFound();
            }

            _context.Collectibles.Remove(collectible);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CollectibleExists(int id)
        {
            return _context.Collectibles.Any(e => e.Id == id);
        }
    }
}
