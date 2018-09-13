using System;
using CommandAndConquer.CLI.Attributes;
using ChallengeAccepted.BLL.Services;

namespace ChallengeAccepted.CLI.Controllers
{
    [CliController("photos", "This is a controller to handle the interactions with photos.")]
    public class PhotoController
    {
        private IPhotoService _service;

        public PhotoController()
        {
            _service = new PhotoService();
        }

        public PhotoController(IPhotoService service)
        {
            _service = service;
        }

        [CliCommand("get", "This method retrieves photos based on the album provided.")]
        public void GetPhotosByAlbum(int album)
        {
            var photos = _service.GetPhotos(album);
            foreach (var photo in photos)
            {
                Console.WriteLine($"[{photo.Id}] {photo.Title}");
            }
        }
    }
}
