using System;

namespace Musicalog.Application.Common.Exceptions
{
    public class AlbumNotFoundException : Exception
    {
        public AlbumNotFoundException(int id)
            : base ($"Can't find album with id {id}")
        {
        }
    }
}
