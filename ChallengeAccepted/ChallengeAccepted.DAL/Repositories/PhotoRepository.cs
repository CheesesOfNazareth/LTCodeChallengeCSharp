using System.Collections.Generic;
using ChallengeAccepted.DAL.Helper;
using ChallengeAccepted.Shared.Models;

namespace ChallengeAccepted.DAL.Repositories
{
    public interface IPhotoRepository
    {
        List<Photo> GetPhotos(int album);
    }

    public class PhotoRepository: IPhotoRepository
    {
        private IApiHelper _api;

        public PhotoRepository()
        {
            _api = new ApiHelper();
            Init();
        }

        public PhotoRepository(IApiHelper apiHelper)
        {
            _api = apiHelper;
            Init();
        }

        private void Init()
        {
            _api.SetUrl("https://jsonplaceholder.typicode.com/photos");
        }

        public List<Photo> GetPhotos(int album)
        {
            var apiParams = $"?albumId={album}";
            return _api.Get(apiParams);
        }
    }
}
