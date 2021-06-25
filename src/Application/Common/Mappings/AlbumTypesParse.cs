using Musicalog.Domain.Enums;
using System;

namespace Musicalog.Application.Common.Mappings
{
    public static class AlbumTypesParse
    {
        public static AlbumType ParseFromString(this string albumType)
        {
            return albumType switch
            {
                "Vinyl" => AlbumType.Vinyl,
                "CD" => AlbumType.CD,
                _ => throw new ArgumentOutOfRangeException(nameof(albumType))
            };
        }
    }
}
