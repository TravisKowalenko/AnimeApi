using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeApi.Models;

namespace AnimeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeShirtsController : ControllerBase
    {
        private readonly AnimeStoreContext _context;

        public AnimeShirtsController(AnimeStoreContext context)
        {
            _context = context;
        }

        // GET: api/AnimeShirts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeShirt>>> GetAnimeShirts()
        {
            return await _context.AnimeShirts.ToListAsync();
        }

        // GET: api/AnimeShirts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimeShirt>> GetAnimeShirt(long id)
        {
            var animeShirt = await _context.AnimeShirts.FindAsync(id);

            if (animeShirt == null)
            {
                return NotFound();
            }

            return animeShirt;
        }

        // PUT: api/AnimeShirts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimeShirt(long id, AnimeShirt animeShirt)
        {
            if (id != animeShirt.Id)
            {
                return BadRequest();
            }

            _context.Entry(animeShirt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimeShirtExists(id))
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

        // POST: api/AnimeShirts
        [HttpPost]
        public async Task<ActionResult<AnimeShirt>> PostAnimeShirt(AnimeShirt animeShirt)
        {
            _context.AnimeShirts.Add(animeShirt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnimeShirt), new { id = animeShirt.Id }, animeShirt);
        }

        // DELETE: api/AnimeShirts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimeShirt(long id)
        {
            var animeShirt = await _context.AnimeShirts.FindAsync(id);
            if (animeShirt == null)
            {
                return NotFound();
            }

            _context.AnimeShirts.Remove(animeShirt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimeShirtExists(long id) => _context.AnimeShirts.Any(e => e.Id == id);
    }
}