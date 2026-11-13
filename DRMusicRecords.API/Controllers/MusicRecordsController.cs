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
        public ActionResult<List<MusicRecord>> GetAllRecords()
        {
            return Ok(_repository.GetAllRecords());
        }
    }
}
