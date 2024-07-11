using Counter.Interfaces.Factorys;
using Counter.Objekte;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace Counter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ADPController : ControllerBase
    {
        const string FILENAME = "apidata.json";

        private readonly ILogger<ADPController> _logger;

        public ADPController(ILogger<ADPController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetADP")]
        public async Task<ActionResult<SafeADP>> Get()
        {
            SafeADP sAdp = IOManager.LoadFileObjectFromJSON<SafeADP>(FILENAME, false);
            List<SafeADP> listAdp = new List<SafeADP>() { sAdp };
            return sAdp;
        }

        [HttpPost(Name = "PostADP")]
        public IActionResult Post([FromBody] ADP data)
        {
            if (data == null)
            {
                return BadRequest("Data is null.");
            }
            SafeADP sAdp = IOManager.LoadFileObjectFromJSON<SafeADP>(FILENAME, false);
            if (sAdp == null)
                sAdp = new SafeADP();
            ApiCommandFactory commandFactory = new ApiCommandFactory();

            try
            {
                commandFactory.ProcessCommand(data.Command).ExecuteCommand(sAdp);
            }
            catch (Exception e)
            {

                return BadRequest($"Es ist ein Fehler entstanden: {e.Message}");
            }
            
            IOManager.SaveObjectToJSON(FILENAME, sAdp, false);
            return Ok(new { received = data });
        }
    }
}
