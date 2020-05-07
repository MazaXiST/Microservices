using CvLab.MicroserviceTemplate2.MessageProcessing;
using Microsoft.AspNetCore.Mvc;

namespace CvLab.MicroserviceTemplate2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public SampleMessage Action(int val)
        {
            return new SampleMessage { Text = $"Sample {val}" };
        }
    }
}
