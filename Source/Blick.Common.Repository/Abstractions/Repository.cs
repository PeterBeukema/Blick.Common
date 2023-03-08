using Microsoft.EntityFrameworkCore;

namespace Blick.Common.Repository.Abstractions;

public abstract class Repository<TRepository> : DbContext
    where TRepository : Repository<TRepository>
{
    protected Repository(DbContextOptions<TRepository> options) : base(options) { }

    public abstract void Initialize();
}