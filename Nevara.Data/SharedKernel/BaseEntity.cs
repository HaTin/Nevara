using System;
using System.Collections.Generic;
using System.Text;

namespace Nevara.Data.SharedKernel
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        // true if domain entity has an entity
        public bool IsTransient()
        {
            return Id.Equals(default(T));
        }
    }
}
