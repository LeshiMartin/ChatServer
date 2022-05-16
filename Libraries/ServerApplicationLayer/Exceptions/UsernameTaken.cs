using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplicationLayer.Exceptions;
public class UsernameTakenException:Exception
{
  public UsernameTakenException ( string userName ) : base ("Username - " + userName + " is already taken")
  {
  }
  
}
