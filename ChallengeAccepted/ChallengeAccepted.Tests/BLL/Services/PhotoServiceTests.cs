using System;
using System.Collections.Generic;
using ChallengeAccepted.BLL.Services;
using ChallengeAccepted.DAL.Helper;
using ChallengeAccepted.DAL.Repositories;
using ChallengeAccepted.Shared.Models;
using Moq;
using NUnit.Framework;

namespace ChallengeAccepted.Tests.BLL.Services
{
    [TestFixture]
    public class PhotoServiceTests
    {
        private Mock<IPhotoRepository> _repo;
        private PhotoService _service;

        [SetUp]
        public void Init()
        {
            _repo = new Mock<IPhotoRepository>();
            _service = new PhotoService(_repo.Object);
        }

        [Test]
        public void AlbumInformationReceivedFromDAL()
        {
            var rand = new Random();
            var album = rand.Next(1, 6);
            var expected = TestPhotos();
            _repo.Setup(r => r.GetPhotos(album)).Returns(expected);

            var actual = _service.GetPhotos(album);

            Assert.AreEqual(expected.Count, actual.Count);
            for (var i = 0; i < expected.Count; i++)
            {
                var a = actual[i];
                var e = expected[i];

                Assert.AreEqual(e.AlbumId, a.AlbumId);
                Assert.AreEqual(e.Id, a.Id);
                Assert.AreEqual(e.ThumbnailUrl, a.ThumbnailUrl);
                Assert.AreEqual(e.Title, a.Title);
                Assert.AreEqual(e.Url, a.Url);
            }
        }

        private List<Photo> TestPhotos()
        {
            return new List<Photo>
            {
                new Photo
                {
                    AlbumId = 1,
                    Id = 1,
                    ThumbnailUrl = "Photo1Thumb",
                    Title = "Photo1",
                    Url = "Photo1Url"
                },
                new Photo
                {
                    AlbumId = 1,
                    Id = 2,
                    ThumbnailUrl = "Photo2Thumb",
                    Title = "Photo2",
                    Url = "Photo2Url"
                },
                new Photo
                {
                    AlbumId = 2,
                    Id = 3,
                    ThumbnailUrl = "Photo3Thumb",
                    Title = "Photo3",
                    Url = "Photo3Url"
                },
                new Photo
                {
                    AlbumId = 3,
                    Id = 4,
                    ThumbnailUrl = "Photo4Thumb",
                    Title = "Photo4",
                    Url = "Photo4Url"
                },
                new Photo
                {
                    AlbumId = 3,
                    Id = 5,
                    ThumbnailUrl = "Photo5Thrumb",
                    Title = "Photo5",
                    Url = "Photo5Url"
                },
            };
        }
    }
}
