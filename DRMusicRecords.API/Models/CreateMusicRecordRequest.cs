using System.ComponentModel.DataAnnotations;

namespace DRMusicRecords.API.Models
{
    public class CreateMusicRecordRequest
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Artist { get; set; } = "";

        [Range(1, int.MaxValue)]
        public int Duration { get; set; }

        [Range(1877, 2027)]
        public int PublicationYear { get; set; }
    }
}
