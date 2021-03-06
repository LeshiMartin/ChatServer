namespace DataAccessLayerAbstraction.Entities;
public abstract class BaseEntity
{
  public Guid Id { get; private set; } = Guid.NewGuid ();
  public DateTime CreatedAt { get; private set; } = DateTime.Now;
}
