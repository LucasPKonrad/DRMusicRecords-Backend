using DRMusicRecords.API.Models;
using DRMusicRecords.API.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DRMusicRecords.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]    
    public class MusicRecordsController : ControllerBase
    {
        private readonly MusicRecordRepository _repository;

        public MusicRecordsController(MusicRecordRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<MusicRecord>> GetAllRecords([FromQuery] string? title, [FromQuery] string? artist) // This method handles GET requests to the /api/musicrecords endpoint. It accepts optional query parameters for title and artist, which are used to filter the records returned by the repository.
        {
            var records = _repository.SearchRecords(title, artist);
            return Ok(records);
        }
    }
}
