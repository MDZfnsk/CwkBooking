using CwkBooking.Api.Services.Abstractions;
using System;

namespace CwkBooking.Api.Services
{
    public class SingletonOperationService : ISingletonOperation
    {
        //private readonly IScopedOperation _scoped;
        //public SingletonOperationService(IScopedOperation scoped)
        //{
        //    _scoped = scoped;            
        //}

        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
