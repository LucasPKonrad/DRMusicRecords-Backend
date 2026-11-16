using DRMusicRecords.API.Models;
using Microsoft.Data.SqlClient;

namespace DRMusicRecords.API.Repositories
{
    public class MusicRecordRepository
    {
        private string _connectionString = "Server=mssql8.unoeuro.com;Database=boan2026_dk_db_lucas;User Id=boan2026_dk ;Password=yDcw6bAE42HxmadgfnGR;TrustServerCertificate=True";

        private List<MusicRecord> _records 
        {
            get {
                List<MusicRecord> records = new List<MusicRecord>();
                string sql = "select * from Records";

                SqlConnection connection = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MusicRecord constructedMusicRecord = new MusicRecord { Id = reader.GetInt32(0), Title = reader.GetString(1), Artist = reader.GetString(2), Duration = reader.GetInt32(3), PublicationYear = reader.GetInt32(4)};
                    records.Add(constructedMusicRecord);
                }

                reader.Close();
                connection.Close();

                return records;
            }
        }

        public List<MusicRecord> GetAllRecords()
        {
            return _records;
        }
        public List<MusicRecord> SearchRecords(string ? title, string ? artist)
        {
            IEnumerable<MusicRecord> result = GetAllRecords(); // IEnumerable is an interface that represents a collection of objects that can be enumerated (iterated) one at a time.

            if (!string.IsNullOrWhiteSpace(title))
            {
                result = result.Where(record =>
                    record.Title.Contains(
                        title,
                        StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(artist))
            {
                result = result.Where(record =>
                    record.Artist.Contains(
                        artist,
                        StringComparison.OrdinalIgnoreCase));
            }

            return result.ToList();
        }

        public MusicRecord AddRecord(MusicRecord record)
        {
            string sql = """
                INSERT INTO Records
            (Title, Artist, Duration, PublicationYear)
            OUTPUT INSERTED.Id
            VALUES
            (@Title, @Artist, @Duration, @PublicationYear)
            """;

            using SqlConnection connection =
                new SqlConnection(_connectionString);

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Title", record.Title);
            command.Parameters.AddWithValue("@Artist", record.Artist);
            command.Parameters.AddWithValue("@Duration", record.Duration);
            command.Parameters.AddWithValue(
                "@PublicationYear", record.PublicationYear);

            connection.Open();

            record.Id = Convert.ToInt32(command.ExecuteScalar());

            return record;
        }
    }
}
