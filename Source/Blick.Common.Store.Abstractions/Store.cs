using Microsoft.EntityFrameworkCore;

namespace Blick.Common.Store.Abstractions;

public abstract class Store<TStore> : DbContext, IStore
    where TStore : Store<TStore>
{
    protected Store(DbContextOptions<TStore> options) : base(options) { }

    public virtual void Initialize()
    {
        // Explicitly do nothing.
    }
}