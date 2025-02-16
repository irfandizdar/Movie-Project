using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly MovieDbContext db = new MovieDbContext();

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            return Ok(await db.Genres.OrderBy(g => g.Name).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var genre = await db.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound($"Genre with Id = {id} not found");
            }

            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] Genre genre)
        {
            db.Genres.Add(genre);
            await db.SaveChangesAsync();
            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] Genre genre)
        {
            if (id != genre.Id)
            {
                return BadRequest("Id mismatch");
            }

            var existingGenre = await db.Genres.FindAsync(id);
            if (existingGenre == null)
            {
                return NotFound("Genre not found");
            }

            existingGenre.Name = genre.Name;

            await db.SaveChangesAsync();

            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await db.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound($"Genre with Id = {id} not found");
            }

            db.Genres.Remove(genre);
            await db.SaveChangesAsync();

            return Ok(id);
        }
    }
}
