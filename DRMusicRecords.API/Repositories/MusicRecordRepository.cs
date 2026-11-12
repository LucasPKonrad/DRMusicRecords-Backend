using DRMusicRecords.API.Models;

namespace DRMusicRecords.API.Repositories
{
    public class MusicRecordRepository
    {
        private readonly List<MusicRecord> _records = new()
        {
            new MusicRecord
            {
                Id = 1,
                Title = "Random Access Memories",
                Artist = "Daft Punk",
                Duration = 444,
                PublicationYear = 2013
            },

            new MusicRecord
            {
                Id = 2,
                Title = "Thriller",
                Artist = "Michael Jackson",
                Duration = 357,
                PublicationYear = 1982
            }
        };

        public List<MusicRecord> GetAll()
        {
            return _records;
        }
    }
}
