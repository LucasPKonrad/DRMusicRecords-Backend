using DRMusicRecords.API.Models;
using Microsoft.AspNetCore.Mvc;
using DRMusicRecords.API.Repositories;

namespace DRMusicRecords.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicRecordsController : ControllerBase
    {
        private readonly MusicRecordRepository _repository;

        public MusicRecordsController(MusicRecordRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<MusicRecord>> GetAllRecords()
        {
            return Ok(_repository.GetAllRecords());
        }
    }
}
