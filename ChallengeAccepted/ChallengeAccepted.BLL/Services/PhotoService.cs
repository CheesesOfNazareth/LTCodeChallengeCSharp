using System.Collections.Generic;
using ChallengeAccepted.DAL.Repositories;
using ChallengeAccepted.Shared.Models;

namespace ChallengeAccepted.BLL.Services
{
    public interface IPhotoService
    {
        List<Photo> GetPhotos(int album);
    }

    public class PhotoService: IPhotoService
    {
        private IPhotoRepository _repo;

        public PhotoService()
        {
            _repo = new PhotoRepository();
        }

        public PhotoService(IPhotoRepository repo)
        {
            _repo = repo;
        }

        public List<Photo> GetPhotos(int album)
        {
            return _repo.GetPhotos(album);
        }
    }
}
