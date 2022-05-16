using DataAccessLayerAbstraction.Entities;

namespace DataAccessLayerAbstraction;
public interface IUserRepository
{
  Task RegisterAsync ( AppUser user, CancellationToken cancellationToken = default );
  Task<AppUser> GetByIdAsync ( Guid id, CancellationToken cancellationToken = default );
  Task<AppUser> GetByUserNameAsync ( string UserName, CancellationToken cancellationToken = default );
  Task<IEnumerable<AppUser>> GetAllUsersAsync ( CancellationToken cancellationToken = default );
  Task UpdateAsync ( AppUser user, CancellationToken cancellationToken = default );
}
