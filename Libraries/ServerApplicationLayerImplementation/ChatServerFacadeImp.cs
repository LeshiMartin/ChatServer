using DataAccessLayerAbstraction;
using DataAccessLayerAbstraction.Entities;
using ServerApplicationLayer;
using ServerApplicationLayer.Dtos.ChatServerDtos;

namespace ServerApplicationLayerImplementation;
internal class ChatServerFacadeImp : ChatServerFacade
{
  private readonly IUserRepository _userRepository;
  private readonly IMessageRepository _messageRepository;

  public ChatServerFacadeImp ( IUserRepository userRepository, IMessageRepository messageRepository )
  {
    _userRepository = userRepository;
    _messageRepository = messageRepository;
  }
  public async Task<IEnumerable<UserDto>> GetAllUsersExceptMeAsync ( Guid userId, CancellationToken cancellationToken = default )
  {
    var users = (AppUser[]) await _userRepository.GetAllUsersAsync (cancellationToken);
    var usersDto = users.Where (x => x.Id != userId).Select (u => new UserDto (u.UserName, u.Id, u.ConnectionId));
    return usersDto;
  }
  public Task<IEnumerable<UserDto>> GetAllUsersInGroupAsync ( int GroupId, CancellationToken cancellationToken = default ) => throw new NotImplementedException ();
  public async Task<IEnumerable<MessageDto>> GetMessageDtosAsync ( Guid userId, CancellationToken cancellationToken = default )
  {
    var messages = (Message[]) await _messageRepository.GetMessagesForUserAsync (userId, cancellationToken);
    return messages.Select (x => new MessageDto (x.MessageContent, x.From!.UserName, x.To!.UserName, x.CreatedAt, x.IsRead));
  }
  public async Task<UserDto> GetUserDtoAsync ( Guid userId, string connectionId, CancellationToken cancellationToken = default )
  {
    var user = await _userRepository.GetByIdAsync (userId, cancellationToken);
    if ( user.ConnectionId != connectionId )
    {
      user.SetConnectionId (connectionId);
      await _userRepository.UpdateAsync (user, cancellationToken);
    }

    return new UserDto (user.UserName, userId, connectionId);
  }
  public async Task MarkMessageAsOpenedAsync ( Guid messageId, CancellationToken cancellationToken = default )
  {
    var message = await _messageRepository.MessageAsync (messageId, cancellationToken);
    message.SetIsRead ();
    await _messageRepository.UpdateMessageAsync (message, cancellationToken);
  }
  public async Task SendMessageAsync ( SendMessageDto dto, CancellationToken cancellationToken = default )
  {
    var message = new Message (dto.Message, dto.FromId, dto.ToId);
    await _messageRepository.InsertMessageAsync (message);
  }
}
