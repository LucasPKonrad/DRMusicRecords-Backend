using DRMusicRecords.API.Repositories;

namespace DRMusicRecords.Tests
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
            var result = repository.GetAllRecords();

            // Assert
            Assert.HasCount(3, result);
        }

        [TestMethod]
        public void GetAll_ContainsCorrectFirstRecord()
        {
            // Arrange
            var repository = new MusicRecordRepository();

            // Act
            var result = repository.GetAllRecords();

            // Assert
            Assert.AreEqual("The Dark Side of the Moon", result[0].Title);
            Assert.AreEqual("Pink Floyd", result[0].Artist);
            Assert.AreEqual(2012, result[0].PublicationYear);
        }

        [TestMethod]
        public void SearchRecords_ByArtist_ReturnsMatchingRecord()
        {
            // Arrange
            var repository = new MusicRecordRepository();

            // Act
            var result = repository.SearchRecords(null, "Pink");

            // Assert
            Assert.HasCount(1, result);
            Assert.AreEqual("Pink Floyd", result[0].Artist);
        }

        [TestMethod]
        public void SearchRecords_ByTitle_IsCaseInsensitive()
        {
            // Arrange
            var repository = new MusicRecordRepository();

            // Act
            var result = repository.SearchRecords("abbey", null);

            // Assert
            Assert.HasCount(1, result);
            Assert.AreEqual("Abbey Road", result[0].Title);
        }

        [TestMethod]
        public void SearchRecords_NoMatch_ReturnsEmptyList()
        {
            // Arrange
            var repository = new MusicRecordRepository();

            // Act
            var result = repository.SearchRecords("Nonexistent", null);

            // Assert
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void SearchRecords_NoFilters_ReturnsAllRecords()
        {
            // Arrange
            var repository = new MusicRecordRepository();

            // Act
            var result = repository.SearchRecords(null, null);

            // Assert
            Assert.HasCount(3, result);
        }
    }
}
