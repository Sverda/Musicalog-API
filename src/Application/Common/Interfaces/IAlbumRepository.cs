using Musicalog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Musicalog.Application.Common.Interfaces
{
    public interface IAlbumRepository
    {
        Task<IEnumerable<Album>> GetListOfAlbums(string albumTitle, string artistName);
    }
}
