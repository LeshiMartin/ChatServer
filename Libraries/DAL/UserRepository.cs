using DataAccessLayerAbstraction;
using DataAccessLayerAbstraction.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;
internal class UserRepository : IUserRepository
{
  private AppDbContext _dbContext;

  public UserRepository ( IDbContextFactory<AppDbContext> dbContextFactory )
  {
    _dbContext = dbContextFactory.CreateDbContext ();
  }
  public async Task<IEnumerable<AppUser>> GetAllUsersAsync ( CancellationToken cancellationToken = default )
  {
    return await _dbContext.Users.ToArrayAsync (cancellationToken);
  }
  public Task<AppUser> GetByIdAsync ( Guid id, CancellationToken cancellationToken = default )
  {
    return _dbContext.Users.SingleAsync (u => u.Id == id, cancellationToken);
  }
  public Task<AppUser> GetByUserNameAsync ( string UserName, CancellationToken cancellationToken = default )
  {
    return _dbContext.Users.SingleAsync (u => u.UserName == UserName, cancellationToken);
  }
  public async Task RegisterAsync ( AppUser user, CancellationToken cancellationToken = default )
  {
    await _dbContext.AddAsync (user, cancellationToken);
    await _dbContext.SaveChangesAsync (cancellationToken);
  }

  public Task UpdateAsync ( AppUser user, CancellationToken cancellationToken = default )
  {
    _dbContext.Users.Update (user);
    return _dbContext.SaveChangesAsync (cancellationToken);
  }
  
}
