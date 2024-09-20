using Microsoft.AspNetCore.Mvc;
using bincom_conbine.Models;

namespace bincom_conbine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController : ControllerBase
    {
        private readonly ResumeModels _resume;

        public ResumeController()
        {
            _resume = new ResumeModels();
        }

        [HttpGet]
        public IActionResult GetResume()
        {
            return Ok(_resume);
        }

    }
}
