using FluentValidation;

namespace ServerApplicationLayer.Dtos.AuthDtos;
public sealed record LoginDto ( string UserName, string Password );

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
  public LoginDtoValidator ()
  {
    RuleFor (x => x.UserName).NotEmpty ();
    RuleFor (x => x.Password).NotEmpty ();
  }
}
