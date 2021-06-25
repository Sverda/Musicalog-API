using Musicalog.Application.Common.Interfaces;
using Musicalog.Domain.Entities;
using Musicalog.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Musicalog.Infrastructure.Persistence
{
    internal class AlbumRepository : IAlbumRepository
    {
        private readonly IDapper _dapper;

        public AlbumRepository(IDapper dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<Album>> GetListOfAlbums(string albumTitle, string artistName)
        {
            var query = @"SELECT *
                          FROM Albums al
                          INNER JOIN Artists ar ON al.ArtistId = ar.Id";

            return await _dapper.GetAllWithOneToMany<Album, Artist>(
                query, 
                (album, artist) => album.Artist = artist);
        }
    }
}
