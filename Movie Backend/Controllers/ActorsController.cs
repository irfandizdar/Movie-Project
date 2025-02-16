using Microsoft.AspNetCore.Mvc;
using Movie_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Movie_Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly MovieDbContext db = new MovieDbContext();

        [HttpGet]
        public async Task<IActionResult> GetAllActors()
        {
            return Ok(await db.Actors.OrderByDescending(a => a.Id).ToListAsync());
        }

        [HttpGet("{parametar}")]
        public async Task<IActionResult> GetActorByName(string parametar)
        {
            var result = await db.Actors.Where(a => a.Name.Contains(parametar)).ToListAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actor = await db.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound($"Actor with Id = {id} not found");
            }

            db.Actors.Remove(actor);
            await db.SaveChangesAsync();

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActor(int id, [FromBody] Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest("Id mismatch");
            }

            var existingActor = await db.Actors.FindAsync(id);
            if (existingActor == null)
            {
                return NotFound("Actor not found");
            }

            existingActor.Name = actor.Name;
            existingActor.BirthDate = actor.BirthDate;
            existingActor.Biography = actor.Biography;
            // Dodajte ostale atribute koje želite ažurirati

            await db.SaveChangesAsync();

            return Ok("Updated");
        }

        [HttpPost]
        public async Task<IActionResult> AddActor([FromBody] Actor actor)
        {
            db.Actors.Add(actor);
            await db.SaveChangesAsync();
            return Ok(actor);
        }
    }
}