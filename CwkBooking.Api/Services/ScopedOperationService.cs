using CwkBooking.Api.Services.Abstractions;
using System;

namespace CwkBooking.Api.Services
{
    public class ScopedOperationService : IScopedOperation
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
