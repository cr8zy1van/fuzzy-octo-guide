using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;
using MusicApi.DataModel;

namespace MusicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaylistController : ControllerBase
    {
        [HttpGet]
        [EnableQuery]
        public IActionResult Get([FromServices] MusicDataContext context, [FromServices] ILogger<PlaylistController> logger)
        {
            return Ok(context.Playlists);
        }
    }
}
