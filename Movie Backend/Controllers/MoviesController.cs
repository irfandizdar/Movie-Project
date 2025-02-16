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
    public class MoviesController : ControllerBase
    {
        private readonly MovieDbContext db = new MovieDbContext();

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            return Ok(await db.Movies
                                .OrderByDescending(m => m.Id)
                                .Select(m => new
                                {
                                    m.Id,
                                    m.Title,
                                    m.PosterUrl,
                                    m.ReleaseDate,
                                    m.DurationMinutes,
                                    Genre = m.Genre.Name // Pretpostavljajući da zanr za filmove postoji kao objekat Genre sa svojim poljem Name
                                })
                                .ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMoviesSwagger()
        {
            return Ok(await db.Movies.OrderByDescending(m => m.Id).ToListAsync());
        }


        [HttpGet("{parametar}")]
        public async Task<IActionResult> GetMovieByTitle(string parametar)
        {
            var result = await db.Movies.Where(m => m.Title.Contains(parametar)).ToListAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await db.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound($"Movie with Id = {id} not found");
            }

            db.Movies.Remove(movie);
            await db.SaveChangesAsync();

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest("Id mismatch");
            }

            var existingMovie = await db.Movies.FindAsync(id);
            if (existingMovie == null)
            {
                return NotFound("Movie not found");
            }

            existingMovie.Title = movie.Title;
            existingMovie.Description = movie.Description;
            // Dodajte ostale atribute koje želite ažurirati

            await db.SaveChangesAsync();

            return Ok("Updated");
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] Movie movie)
        {
            db.Movies.Add(movie);
            await db.SaveChangesAsync();
            return Ok(movie);
        }

        // GET: api/Movies/GetMovieVideoUrl/{id}
        [HttpGet("{id}")]
        public ActionResult<string> GetMovieVideoUrl(int id)
        {
            var movie = db.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie.VideoUrl); // Vraća URL adrese videozapisa (npr. YouTube trailer)
        }

    }
}
