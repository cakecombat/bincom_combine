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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Picture>>> GetPictures()
        {
            return await _context.Pictures.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Picture>> GetPicture(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);

            if (picture == null)
            {
                return NotFound();
            }

            return picture;
        }
        [HttpPost]
        public async Task<ActionResult<Picture>> UploadPicture([FromForm] IFormFile file, [FromForm] string name, [FromForm] string description)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var picture = new Picture
            {
                Name = name,
                Description = description,
                Image = memoryStream.ToArray()
            };

            _context.Pictures.Add(picture);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPicture), new { id = picture.Id }, picture);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
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
