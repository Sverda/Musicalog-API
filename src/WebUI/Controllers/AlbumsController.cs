using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Musicalog.WebUI.Dto;
using Musicalog.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Linq;

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
        public void Post([FromBody] AlbumDto value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AlbumDto value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
