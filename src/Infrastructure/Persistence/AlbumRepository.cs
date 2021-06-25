using Dapper;
using Musicalog.Application.Common.Interfaces;
using Musicalog.Domain.Entities;
using Musicalog.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Data;
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

        public async Task Add(Album album)
        {
            var query = @"INSERT INTO dbo.Albums (Title, ArtistId, Type, Stock) VALUES (@Title, @ArtistId, @Type, @Stock);
                          SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var parms = new DynamicParameters();
            parms.AddDynamicParams(album);
            await _dapper.Insert<int>(query, parms, CommandType.Text);
        }
    }
}
