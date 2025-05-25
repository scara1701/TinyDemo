using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyDemo.SharedLib.Services;


namespace TinyDemo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LottoController : ControllerBase
    {
        ILottoService _lottoService;

        public LottoController(ILottoService lottoService)
        {
            _lottoService = lottoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var lotto = await _lottoService.GenerateLotto();
                return Ok(lotto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Server Exception",
                    Details = ex.Message
                });
            }
        }
    }
}
