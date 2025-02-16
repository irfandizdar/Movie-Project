using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Backend.Controllers { 

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly MovieDbContext db = new MovieDbContext();

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            return Ok(await db.Reviews.OrderByDescending(r => r.Id).ToListAsync());
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetReviewByMovieId(int movieId)
        {
            var result = await db.Reviews.Where(r => r.MovieId == movieId).ToListAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await db.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound($"Review with Id = {id} not found");
            }

            db.Reviews.Remove(review);
            await db.SaveChangesAsync();

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] Review review)
        {
            if (id != review.Id)
            {
                return BadRequest("Id mismatch");
            }

            var existingReview = await db.Reviews.FindAsync(id);
            if (existingReview == null)
            {
                return NotFound("Review not found");
            }

            existingReview.MovieId = review.MovieId;
            existingReview.UserId = review.UserId;
            existingReview.Rating = review.Rating;
            existingReview.Comment = review.Comment;
            // Dodajte ostale atribute koje želite ažurirati

            await db.SaveChangesAsync();

            return Ok("Updated");
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] Review review)
        {
            db.Reviews.Add(review);
            await db.SaveChangesAsync();
            return Ok(review);
        }
    }
}
