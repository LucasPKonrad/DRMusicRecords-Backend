using DRMusicRecords.API.Models;
using DRMusicRecords.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DRMusicRecords.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DRMusicRecordsController : ControllerBase
    {
        private readonly MusicRecordRepository _repository;

        public DRMusicRecordsController(MusicRecordRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<MusicRecord>> GetAll()
        {
            return Ok(_repository.GetAll());
        }
    }
    
    
}
