using Dapper;
using Musicalog.Application.Common.Exceptions;
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

        public async Task<Album> FindOne(int id)
        {
            var query = @"SELECT TOP 1 * FROM Albums WHERE Id = @Id";
            var parms = new DynamicParameters(new { Id = id });
            return await _dapper.Get<Album>(query, parms, CommandType.Text);
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
            var parms = new DynamicParameters(album);
            await _dapper.Insert<int>(query, parms, CommandType.Text);
        }

        public async Task Update(int id, Album album)
        {
            _ = await FindOne(id) ?? throw new AlbumNotFoundException(id);
            album.Id = id;

            var query = @"UPDATE dbo.Albums SET Title = @Title, ArtistId = @ArtistId, Type = @Type, Stock = @Stock 
                          WHERE Id = @Id";
            var parms = new DynamicParameters(album);
            await _dapper.Update(query, parms, CommandType.Text);
        }
    }
}
