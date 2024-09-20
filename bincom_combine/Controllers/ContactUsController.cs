using bincom_combine.Models;
using bincom_conbine.Data;
using bincom_conbine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bincom_conbine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactUsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Submits a contact form entry to the database.
        /// </summary>
        /// <param name="contactUs">The contact details from the user.</param>
        /// <returns>A success or failure message.</returns>
        [HttpPost]
        public async Task<IActionResult> SubmitContact([FromBody] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                _context.ContactUsEntries.Add(contactUs);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Contact form submitted successfully." });
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Retrieves all contact form entries.
        /// </summary>
        /// <returns>A list of all contact form submissions.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllContactSubmissions()
        {
            var submissions = await _context.ContactUsEntries.ToListAsync();
            return Ok(submissions);
        }
    }
}
