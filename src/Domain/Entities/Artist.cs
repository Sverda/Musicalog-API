﻿namespace Musicalog.Domain.Entities
{
    public sealed class Artist : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
