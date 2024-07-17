using Counter.Authorization.ApiController;
using Counter.Interfaces.Factorys;
using Counter.Objekte;
using Microsoft.AspNetCore.Mvc;

namespace Counter.Controllers
{
    [ApiKey]
    [ApiController]
    [Route("[controller]")]
    public class ADPController : Controller
    {
        const string FILENAME = "apidata.json";

        private readonly ILogger<ADPController> _logger;

        public ADPController(ILogger<ADPController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetADP")]
        public  IActionResult Get()
        {
            SafeADP sAdp = IOManager.LoadFileObjectFromJSON<SafeADP>(FILENAME, false);
            List<SafeADP> listAdp = new List<SafeADP>() { sAdp };
			JsonResult result = new JsonResult(
                new {
                    Beauftragung = sAdp.Beauftragung, 
                    Buchung = sAdp.Buchung  
                });
			return result;
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
