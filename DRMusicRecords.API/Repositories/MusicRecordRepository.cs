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
    }
}
