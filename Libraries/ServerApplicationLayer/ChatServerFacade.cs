using ServerApplicationLayer.Dtos.ChatServerDtos;

namespace ServerApplicationLayer;
public interface ChatServerFacade
{
  Task<UserDto> GetUserDtoAsync ( Guid userId, string connectionId, CancellationToken cancellationToken = default );
  Task<IEnumerable<UserDto>> GetAllUsersExceptMeAsync ( Guid userId, CancellationToken cancellationToken = default );
  Task<IEnumerable<UserDto>> GetAllUsersInGroupAsync ( int GroupId, CancellationToken cancellationToken = default );
  Task SendMessageAsync ( SendMessageDto dto, CancellationToken cancellationToken = default );
  Task<IEnumerable<MessageDto>> GetMessageDtosAsync ( Guid userId, CancellationToken cancellationToken = default );
  Task MarkMessageAsOpenedAsync ( Guid messageId, CancellationToken cancellationToken = default );
}
