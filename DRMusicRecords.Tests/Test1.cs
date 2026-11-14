using DRMusicRecords.API.Models;
using DRMusicRecords.API.Repositories;

namespace DRMusicRecords.Tests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void MusicRecordRepoGetAllTest()
        {
            MusicRecordRepository repo = new MusicRecordRepository();
            List<MusicRecord> records = repo.GetAllRecords();

            Assert.IsNotNull(records);
            Assert.AreEqual(3,records.Count);

            MusicRecord first = records[0];
            Assert.AreEqual(1, first.Id);
            Assert.AreEqual("The Dark Side of the Moon", first.Title);
            Assert.AreEqual("Pink Floyd", first.Artist);
            Assert.AreEqual(244, first.Duration);
            Assert.AreEqual(2012, first.PublicationYear);
        }
    }
}
