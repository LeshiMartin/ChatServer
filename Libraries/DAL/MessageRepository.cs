using DataAccessLayerAbstraction;
using DataAccessLayerAbstraction.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;
internal sealed class MessageRepository : IMessageRepository
{
  private AppDbContext _dbContext;

  public MessageRepository ( IDbContextFactory<AppDbContext> dbContextFactory )
  {
    _dbContext = dbContextFactory.CreateDbContext ();

  }
  public async Task<IEnumerable<Message>> GetMessagesForUserAsync ( Guid userId, CancellationToken cancellationToken = default )
  {
    return await _dbContext.Messages
      .Include (x => x.From)
      .Include (x => x.To)
      .Where (m => m.ToId == userId || m.FromId == userId)
      .ToArrayAsync (cancellationToken);
  }
  public async Task InsertMessageAsync ( Message message, CancellationToken cancellationToken = default )
  {
    await _dbContext.Messages.AddAsync (message, cancellationToken);
    await _dbContext.SaveChangesAsync (cancellationToken);
  }
  public Task<Message> MessageAsync ( Guid id, CancellationToken cancellationToken = default )
  {
    return _dbContext.Messages.SingleAsync (m => m.Id == id, cancellationToken);
  }
  public Task RemoveMessageAsync ( Message message, CancellationToken cancellationToken = default )
  {
    _dbContext.Messages.Remove (message);
    return _dbContext.SaveChangesAsync (cancellationToken);
  }
  public Task UpdateMessageAsync ( Message message, CancellationToken cancellationToken = default )
  {
    _dbContext.Messages.Update (message);
    return _dbContext.SaveChangesAsync (cancellationToken);
  }
}
