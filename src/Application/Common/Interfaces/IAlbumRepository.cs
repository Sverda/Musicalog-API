using Musicalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Musicalog.Application.Common.Interfaces
{
    public interface IAlbumRepository
    {
        Task<Album> FindOne(int id);

        Task<IEnumerable<Album>> GetListOfAlbums(string albumTitle, string artistName);

        Task Add(Album album);

        Task Update(int id, Album album);
    }
}
