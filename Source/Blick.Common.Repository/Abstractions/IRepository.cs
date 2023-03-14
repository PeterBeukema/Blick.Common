using Microsoft.EntityFrameworkCore.Storage;

namespace Blick.Common.Repository.Abstractions;

public interface IRepository
{
    public int SaveChanges();
    public IDbContextTransaction BeginTransaction();
}