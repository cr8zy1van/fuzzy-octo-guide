using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Logging;
using MusicApi.DataModel;

namespace MusicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        [HttpGet]
        [EnableQuery]
        public IActionResult Get([FromServices] MusicDataContext context, [FromServices] ILogger<PlaylistController> logger)
        {
            try
            {
                return Ok(context.Artists);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error in Get Artist");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An Error has occured");
            }
        }
    }
}