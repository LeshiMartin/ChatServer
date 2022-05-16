using Microsoft.AspNetCore.Identity;

namespace DataAccessLayerAbstraction.Entities;
public sealed class AppUser : BaseEntity
{

  private PasswordHasher<AppUser> _passwordHasher = new PasswordHasher<AppUser> ();
  public string UserName { get; private set; }
  public string Password { get; private set; }
  public string? ConnectionId { get; private set; }

  private readonly List<Message> _receivedMessages = new ();
  public IReadOnlyCollection<Message> ReceivedMessages => _receivedMessages.AsReadOnly ();

  private readonly List<Message> _sentMessages = new ();
  public IReadOnlyCollection<Message> SentMessages => _sentMessages.AsReadOnly ();
  public AppUser ( string userName, string password )
  {
    UserName = userName;
    Password = _passwordHasher.HashPassword (this, password);
  }

  public AppUser SetConnectionId ( string connectionId )
  {
    ConnectionId = connectionId;
    return this;
  }
  public bool validatePassword ( string password )
  {
    return _passwordHasher.VerifyHashedPassword (this, Password, password) == PasswordVerificationResult.Success;
  }
  private AppUser () { }
}
