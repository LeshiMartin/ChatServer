namespace DataAccessLayerAbstraction.Entities;
public sealed class Message : BaseEntity
{
  private Message ()
  {

  }

  public Message ( string text, Guid FromUserId, Guid ToUserId )
  {
    MessageContent = text;
    FromId = FromUserId;
    ToId = ToUserId;
  }
  public string MessageContent { get; private set; }
  public bool IsRead { get; private set; }
  public DateTime? DateOppened { get; set; }
  public Guid FromId { get; set; }
  public Guid ToId { get; set; }
  public AppUser? From { get; set; }
  public AppUser? To { get; set; }
  public Message SetIsRead ()
  {
    IsRead = true;
    return this;
  }
}
