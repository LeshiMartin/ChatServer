using DataAccessLayerAbstraction.Entities;

namespace DataAccessLayerAbstraction;
public interface IMessageRepository
{
  Task<Message> MessageAsync ( Guid id, CancellationToken cancellationToken = default );
  Task<IEnumerable<Message>> GetMessagesForUserAsync ( Guid userId, CancellationToken cancellationToken = default );
  Task InsertMessageAsync ( Message message, CancellationToken cancellationToken = default );
  Task UpdateMessageAsync ( Message message, CancellationToken cancellationToken = default );
  Task RemoveMessageAsync ( Message message, CancellationToken cancellationToken = default );
}
