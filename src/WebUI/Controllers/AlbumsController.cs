using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Musicalog.WebUI.Dto;
using Musicalog.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using Musicalog.Domain.Entities;
using Musicalog.Application.Common.Mappings;

namespace Musicalog.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumsController(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<AlbumDto>> GetAsync()
        {
            var dbAlbums = await _albumRepository.GetListOfAlbums(null, null);
            var dto = dbAlbums.Select(a => new AlbumDto
            {
                Title = a.Title, 
                ArtistName = a.Artist?.Name,
                AlbumType = a.Type.ToString(),
                Stock = a.Stock
            });
            return dto;
        }

        [HttpPost]
        public async Task PostAsync([FromBody] CreateAlbumDto value)
        {
            var album = new Album
            {
                Title = value.Title,
                ArtistId = value.ArtistId,
                Stock = value.Stock,
                Type = value.AlbumType.ParseFromString()
            };
            await _albumRepository.Add(album);
        }

        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] UpdateAlbumDto value)
        {
            var album = new Album
            {
                Title = value.Title,
                ArtistId = value.ArtistId,
                Stock = value.Stock,
                Type = value.AlbumType.ParseFromString()
            };
            await _albumRepository.Update(id, album);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
