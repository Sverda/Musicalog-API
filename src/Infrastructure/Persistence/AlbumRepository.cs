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
            var query = "SELECT * FROM Albums";

            return await _dapper.GetAll<Album>(query, null, CommandType.Text);
        }
    }
}
