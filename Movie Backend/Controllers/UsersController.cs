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
    public class UsersController : ControllerBase
    {
        private readonly MovieDbContext db = new MovieDbContext(); 

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await db.Users.OrderByDescending(u => u.Id).ToListAsync());
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var result = await db.Users.Where(u => u.Username.Contains(username)).ToListAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with Id = {id} not found");
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest("Id mismatch");
            }

            var existingUser = await db.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;
            // Dodajte ostale atribute koje želite ažurirati

            await db.SaveChangesAsync();

            return Ok("Updated");
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}
