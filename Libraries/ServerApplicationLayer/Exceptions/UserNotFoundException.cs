namespace ServerApplicationLayer.Exceptions;
public class UserNotFoundException : Exception
{
  public UserNotFoundException ( string userName ) : base ("No user with name " + userName + " found")
  {
  }
}
