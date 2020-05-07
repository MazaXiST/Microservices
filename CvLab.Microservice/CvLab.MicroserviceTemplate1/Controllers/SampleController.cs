using CvLab.MicroserviceTemplate1.MessageProcessing;
using Microsoft.AspNetCore.Mvc;

namespace CvLab.MicroserviceTemplate1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public SampleMessage1 Action(int val)
        {
            return new SampleMessage1 { Text = $"Sample {val}" };
        }
    }
}
