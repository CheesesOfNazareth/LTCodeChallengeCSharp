using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ChallengeAccepted.BLL.Services;
using ChallengeAccepted.CLI.Controllers;
using ChallengeAccepted.Shared.Models;
using Moq;
using NUnit.Framework;

namespace ChallengeAccepted.Tests.CLI.Controllers
{
    [TestFixture]
    public class PhotoControllerTest
    {
        private Mock<IPhotoService> _service;
        private PhotoController _controller;

        //Stuff for dealing with console output
        protected StringWriter consoleMock;
        protected StringBuilder mockConsole = new StringBuilder();

        [SetUp]
        public void Init()
        {
            consoleMock = new StringWriter(mockConsole);
            Console.SetOut(consoleMock);

            _service = new Mock<IPhotoService>();
            _controller = new PhotoController(_service.Object);
        }

        [Test]
        public void InformationSentToBLL()
        {
            var album = 1;
            _service.Setup(s => s.GetPhotos(album)).Returns(new List<Photo>());
            _controller.GetPhotosByAlbum(album);
            _service.Verify(s => s.GetPhotos(album), Times.Once);
        }

        [Test]
        public void GetPhotosOutputsIdAndTitle()
        {
            var album = 1;
            mockConsole.Clear();
            _service.Setup(s => s.GetPhotos(album)).Returns(TestPhotos().Where(p => p.AlbumId == album).ToList);
            var consoleLines = new List<string>
            {
                "[1] Photo1",
                "[2] Photo2",
            };

            _controller.GetPhotosByAlbum(album);

            var actual = mockConsole.ToString();
            var expectedString = ConvertConsoleLinesToString(consoleLines);
            Assert.AreEqual(expectedString, actual);
        }
        
        private string ConvertConsoleLinesToString(List<string> lines, bool startingNewLine = false, bool endingNewLine = true)
        {
            var consoleString = string.Join(Environment.NewLine, lines);
            if (endingNewLine) consoleString += Environment.NewLine;
            if (startingNewLine) consoleString = Environment.NewLine + consoleString;
            return consoleString;
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
