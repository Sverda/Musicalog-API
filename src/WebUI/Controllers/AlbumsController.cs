using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Musicalog.WebUI.Dto;

namespace Musicalog.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<AlbumDto> Get()
        {
            return new[]
            {
                new AlbumDto(),
                new AlbumDto(),
                new AlbumDto()
            };
        }

        [HttpGet("{id}")]
        public AlbumDto Get(int id)
        {
            return new AlbumDto();
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
