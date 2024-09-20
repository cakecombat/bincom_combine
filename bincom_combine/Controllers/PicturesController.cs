using Microsoft.AspNetCore.Mvc;
using bincom_conbine.Data;
using bincom_conbine.Models;
using Microsoft.EntityFrameworkCore;

namespace bincom_conbine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PicturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all pictures stored in the database.
        /// </summary>
        /// <returns>A list of pictures.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Picture>>> GetPictures()
        {
            // Retrieve all pictures from the database
            return await _context.Pictures.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific picture by its ID.
        /// </summary>
        /// <param name="id">The ID of the picture.</param>
        /// <returns>The requested picture or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Picture>> GetPicture(int id)
        {
            // Find the picture by its ID
            var picture = await _context.Pictures.FindAsync(id);

            if (picture == null)
            {
                return NotFound();
            }

            return picture;
        }

        /// <summary>
        /// Uploads a new picture and stores it in the database.
        /// </summary>
        /// <param name="file">The image file to be uploaded.</param>
        /// <param name="name">The name of the picture.</param>
        /// <param name="description">The description of the picture.</param>
        /// <returns>The newly created picture details.</returns>
        [HttpPost]
        public async Task<ActionResult<Picture>> UploadPicture([FromForm] IFormFile file, [FromForm] string name, [FromForm] string description)
        {
            // Check if a file has been uploaded
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Convert the uploaded file to byte array to store in the database
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            // Create a new picture entity
            var picture = new Picture
            {
                Name = name,
                Description = description,
                Image = memoryStream.ToArray()
            };

            // Add the new picture to the database
            _context.Pictures.Add(picture);
            await _context.SaveChangesAsync();

            // Return the created picture with a 201 Created status
            return CreatedAtAction(nameof(GetPicture), new { id = picture.Id }, picture);
        }

        /// <summary>
        /// Deletes a specific picture by its ID.
        /// </summary>
        /// <param name="id">The ID of the picture to delete.</param>
        /// <returns>No content on successful deletion, or 404 if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            // Find the picture by its ID
            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
