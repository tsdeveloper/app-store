using System;

namespace Core.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsCanceled { get; set; }
    }
}