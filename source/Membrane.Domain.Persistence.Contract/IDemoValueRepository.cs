
using System;
using Membrane.Domain.Entity;
using Membrane.Domain.Store;

namespace Membrane.Domain.Persistence
{
    /// <summary>
    /// The marker interface for <see cref="DemoValue"/> repositories.
    /// </summary>
    public interface IDemoValueRepository
        : IRepository<DemoValue, Guid> { }
}
