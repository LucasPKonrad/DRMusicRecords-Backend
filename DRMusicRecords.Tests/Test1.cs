using Microsoft.VisualStudio.TestTools.UnitTesting;
using DRMusicRecords.API.Models;
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
            Assert.IsTrue(result.Count > 0);
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
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void AddRecord_ShouldGenerateId()
        {
            // Arrange
            var repository = new MusicRecordRepository();

            var record = new MusicRecord
            {
                Title = "MSTest Album",
                Artist = "Test Artist",
                Duration = 180,
                PublicationYear = 2020
            };

            // Act
            var result = repository.AddRecord(record);

            // Assert
            Assert.IsTrue(result.Id > 0);
            Assert.AreEqual("MSTest Album", result.Title);
            Assert.AreEqual("Test Artist", result.Artist);
        }

        [TestMethod]
        public void AddRecord_ShouldPersistInDatabase()
        {
            // Arrange
            var repository = new MusicRecordRepository();

            var record = new MusicRecord
            {
                Title = "MSTest Persistence",
                Artist = "Test Artist",
                Duration = 200,
                PublicationYear = 2020
            };

            // Act
            var createdRecord = repository.AddRecord(record);

            var allRecords = repository.GetAllRecords();

            // Assert
            var savedRecord = allRecords
                .FirstOrDefault(r => r.Id == createdRecord.Id);

            Assert.IsNotNull(savedRecord);
            Assert.AreEqual("MSTest Persistence", savedRecord.Title);
            Assert.AreEqual("Test Artist", savedRecord.Artist);
            Assert.AreEqual(200, savedRecord.Duration);
            Assert.AreEqual(2020, savedRecord.PublicationYear);
        }

        [TestMethod]
        public void DeleteRecord_ShouldRemoveRecord()
        {
            // Arrange
            var repository = new MusicRecordRepository();

            var record = repository.AddRecord(new MusicRecord
            {
                Title = "Delete Test",
                Artist = "MSTest",
                Duration = 120,
                PublicationYear = 2020
            });

            // Act
            bool deleted = repository.DeleteRecord(record.Id);

            // Assert
            Assert.IsTrue(deleted);

            var records = repository.GetAllRecords();

            Assert.IsFalse(records.Any(r => r.Id == record.Id));
        }

        [TestMethod]
        public void DeleteRecord_NonExistingId_ReturnsFalse()
        {
            var repository = new MusicRecordRepository();

            bool result = repository.DeleteRecord(-1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateRecord_ShouldChangeRecord()
        {
            // Arrange
            var repository = new MusicRecordRepository();

            var record = repository.AddRecord(new MusicRecord
            {
                Title = "Original Title",
                Artist = "Test Artist",
                Duration = 180,
                PublicationYear = 2020
            });

            try
            {
                // Act
                record.Title = "Updated Title";
                record.Duration = 240;

                bool updated = repository.UpdateRecord(record);

                // Assert
                Assert.IsTrue(updated);

                var savedRecord = repository.GetAllRecords()
                    .FirstOrDefault(r => r.Id == record.Id);

                Assert.IsNotNull(savedRecord);
                Assert.AreEqual("Updated Title", savedRecord.Title);
                Assert.AreEqual(240, savedRecord.Duration);
            }
            finally
            {
                // Remove our test record
                repository.DeleteRecord(record.Id);
            }
        }

        [TestMethod]
        public void UpdateRecord_NonExistingId_ReturnsFalse()
        {
            // Arrange
            var repository = new MusicRecordRepository();

            var record = new MusicRecord
            {
                Id = -1,
                Title = "Test",
                Artist = "Test Artist",
                Duration = 180,
                PublicationYear = 2020
            };

            // Act
            bool updated = repository.UpdateRecord(record);

            // Assert
            Assert.IsFalse(updated);
        }
    }
}
