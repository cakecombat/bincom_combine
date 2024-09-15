using Microsoft.AspNetCore.Mvc;
using bincom_conbine.Models;

namespace bincom_conbine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController : ControllerBase
    {
        // Create an instance of the ResumeModels
        private readonly ResumeModels _resume;

        public ResumeController()
        {
            // Initialize with default data
            _resume = new ResumeModels();
        }

        // GET: api/resume
        [HttpGet]
        public IActionResult GetResume()
        {
            // Return the resume data as JSON
            return Ok(_resume);
        }

        // Additional endpoints can be added here, such as for updating or modifying the resume data if needed
    }
}
