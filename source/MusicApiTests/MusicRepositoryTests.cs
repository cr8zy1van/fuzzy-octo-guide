using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Moq.AutoMock;
using MusicApi.DataModel;
using NUnit.Framework;

namespace MusicApiTests
{
    public class MusicRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Mock_Repository_Query_Success()
        {
            Mock<IMusicRepository> musicRepoMock = new Mock<IMusicRepository>();
            musicRepoMock.Setup(x => x.Set<Track>()).Returns(GetTestTracks());
            List<Track> testTracks = musicRepoMock.Object.Set<Track>().ToList();
            Assert.True(testTracks.Count == 5, "Repository mock should return 5 test tracks");
        }

        private IQueryable<Track> GetTestTracks(int number = 5)
        {
            Album moqAlbum = new Album()
            {
                Title = "Test Album"
            };

            return Enumerable.Range(0, number).Select(x => new Track()
            {
                TrackId = x,
                Name = $"{Guid.NewGuid()}",
                Composer = "Composer",
                Genre = new Genre()
                {
                    Name = "Test Genre"
                },
                Album = moqAlbum
            }).AsQueryable();
        }
    }
}