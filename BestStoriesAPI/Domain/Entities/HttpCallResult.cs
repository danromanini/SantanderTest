using System;

namespace Domain.Entities
{
    public class HttpCallResult<T>
    {
        public T Result { get; set; }
        public Exception Exception { get; set; }
        public bool Success { get; set; }
    }
}
