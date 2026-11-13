using DRMusicRecords.API.Models;

namespace DRMusicRecords.API.Repositories
{
    public class MusicRecordRepository
    {
        public readonly List<MusicRecord> _records = new()
        {
            new MusicRecord
            {
                Id = 1,
                Title = "The Dark Side of the Moon",
                Artist = "Pink Floyd",
                Duration = 244,
                PublicationYear = 2012
            },
            new MusicRecord
            {
                Id = 2,
                Title = "Abbey Road",
                Artist = "The Beatles",
                Duration = 185,
                PublicationYear = 1969
            },
            new MusicRecord
            {
                Id = 3,
                Title = "Thriller",
                Artist = "Michael Jackson",
                Duration = 258,
                PublicationYear = 1982
            }
        };

        public List<MusicRecord> GetAllRecords()
        {
            return _records;
        }
    }
}
