using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using ChallengeAccepted.DAL.Helper;
using ChallengeAccepted.DAL.Repositories;
using ChallengeAccepted.Shared.Models;
using Moq;
using NUnit.Framework;

namespace ChallengeAccepted.Tests.DAL.Repositories
{
    [TestFixture]
    public class PhotoRepositoryTests
    {
        private Mock<IApiHelper> _apiMock;
        private PhotoRepository _repo;

        [SetUp]
        public void Init()
        {
            _apiMock = new Mock<IApiHelper>();
            _repo = new PhotoRepository(_apiMock.Object);
        }

        [Test]
        public void PhotoRepositoryCallsSetupUrl()
        {
            var apiMock = new Mock<IApiHelper>();
            var repo = new PhotoRepository(apiMock.Object);

            apiMock.Verify(a => a.SetUrl("https://jsonplaceholder.typicode.com/photos"), Times.Once);
        }

        [Test]
        public void GetPhotosCallsApi()
        {
            var album = 1;
            _repo.GetPhotos(album);

            _apiMock.Verify(a => a.Get($"?albumId={album}"), Times.Once);
        }

        [Test]
        public void GetPhotosReturnsPhotos()
        {
            var album = 1;
            _apiMock.Setup(a => a.Get($"?albumId={album}")).Returns(TestPhotos());

            _repo.GetPhotos(album);

            _apiMock.Verify(a => a.Get($"?albumId={album}"), Times.Once);
        }

        [Test]
        public void GetPhotosReturnsPhotosForAlbumCalled()
        {
            var album = 1;
            var expected = TestPhotos().Where(p => p.AlbumId == album).ToList();
            _apiMock.Setup(a => a.Get($"?albumId={album}")).Returns(expected);

            var actual = _repo.GetPhotos(album);

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

        [Test]
        public void GetPhotosPassesCorrectApiParameters()
        {
            var album = 1;

            _repo.GetPhotos(album);
            
            _apiMock.Verify(a => a.Get($"?albumId={album}"), Times.Once);
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
