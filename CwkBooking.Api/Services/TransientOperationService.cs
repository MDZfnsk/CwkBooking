using CwkBooking.Api.Services.Abstractions;
using System;

namespace CwkBooking.Api.Services
{
    public class TransientOperationService : ITransientOperation
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
