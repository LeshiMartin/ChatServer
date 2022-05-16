using DataAccessLayerAbstraction;
using DataAccessLayerAbstraction.Entities;
using ServerApplicationLayer;
using ServerApplicationLayer.Dtos.AuthDtos;
using ServerApplicationLayer.Exceptions;
using ServerApplicationLayerImplementation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplicationLayerImplementation;
internal class AuthFacadeImpl : AuthFacade
{
  private readonly IUserRepository _userRepository;
  private readonly IJwtManager _jwtManager;

  public AuthFacadeImpl ( IUserRepository userRepository, IJwtManager jwtManager )
  {
    _userRepository = userRepository;
    _jwtManager = jwtManager;
  }
  public async Task<IdentityResponse> LoginAsync ( LoginDto dto, CancellationToken cancellationToken = default )
  {
    var user = await _userRepository.GetByUserNameAsync (dto.UserName, cancellationToken);
    if ( user is null )
      throw new UserNotFoundException (dto.UserName);
    if ( !user.validatePassword (dto.Password) )
      throw new WrongPasswordException ();
    var token = _jwtManager.CreateToken (user.UserName);
    return new IdentityResponse (token, DateTime.Now.AddDays (7));
  }
  public async Task RegisterAsync ( RegisterDto dto, CancellationToken cancellationToken = default )
  {
    var user = await _userRepository.GetByUserNameAsync (dto.UserName, cancellationToken);
    if ( user is not null )
      throw new UsernameTakenException (dto.UserName);
    user = new AppUser (dto.UserName, dto.Password);
    await _userRepository.RegisterAsync (user, cancellationToken);
  }
}
