using Blick.Common.Store.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Blick.Common.Store;

public class Store<TStore> : DbContext, IStore
    where TStore : Store<TStore>
{
    public Store(DbContextOptions<TStore> options) : base(options) { }

    public virtual void Initialize()
    {
        // Explicitly do nothing.
    }
}