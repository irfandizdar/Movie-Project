using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Backend.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly MovieDbContext db = new MovieDbContext();

        [HttpGet]
        public async Task<IActionResult> GetAllSeries()
        {
            return Ok(await db.Series
                                .OrderByDescending(s => s.Id)
                                .Select(s => new
                                {
                                    s.Id,
                                    s.Title,
                                    s.PosterUrl,
                                    s.NumberOfSeasons,
                                    Genre = s.Genre.Name, // Pretpostavljajući da zanr za serije postoji kao objekat Genre sa svojim poljem Name
                                    FirstAirDate = s.FirstAirDate.HasValue ? s.FirstAirDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : "",// Ukljucujemo godinu izlaska
                                })
                                .ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> GetAllSeriesSwagger()
        {
            return Ok(await db.Series.OrderByDescending(s => s.Id).ToListAsync());
        }
        [HttpGet("{parametar}")]
        public async Task<IActionResult> GetSeriesByTitle(string parametar)
        {
            var result = await db.Series.Where(s => s.Title.Contains(parametar)).ToListAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeries(int id)
        {
            var series = await db.Series.FindAsync(id);
            if (series == null)
            {
                return NotFound($"Series with Id = {id} not found");
            }

            db.Series.Remove(series);
            await db.SaveChangesAsync();

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeries(int id, [FromBody] Series series)
        {
            if (id != series.Id)
            {
                return BadRequest("Id mismatch");
            }

            var existingSeries = await db.Series.FindAsync(id);
            if (existingSeries == null)
            {
                return NotFound("Series not found");
            }

            existingSeries.Title = series.Title;
            existingSeries.Description = series.Description;
            // Dodajte ostale atribute koje želite ažurirati

            await db.SaveChangesAsync();

            return Ok("Updated");
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeries([FromBody] Series series)
        {
            db.Series.Add(series);
            await db.SaveChangesAsync();
            return Ok(series);
        }

        // GET: api/Series/GetSeriesVideoUrl/{id}
       /* [HttpGet]
        public ActionResult<string> GetSeriesVideoUrl(int id)
        {
            var series = db.Series.FirstOrDefault(s => s.Id == id);

            if (series == null)
            {
                return NotFound();
            }

            return Ok(series.VideoUrl); // Vraća URL adrese videozapisa (npr. YouTube trailer)
        }*/

    }
}
