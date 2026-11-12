using DRMusicRecords.API.Repositories;
namespace TestDRMusic
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void GetAll_ReturnsTwoRecords()
        {
            // Arrange
            var repository = new MusicRecordRepository();

            // Act
            var result = repository.GetAll();

            // Assert
            Assert.HasCount(2, result);
        }

        [TestMethod]
        public void GetAll_ContainsCorrectFirstRecord()
        {
            // Arrange
            var repository = new MusicRecordRepository();

            // Act
            var result = repository.GetAll();

            // Assert
            Assert.AreEqual("Random Access Memories", result[0].Title);
            Assert.AreEqual("Daft Punk", result[0].Artist);
            Assert.AreEqual(2013, result[0].PublicationYear);
        }
    }
}
