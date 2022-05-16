using FluentValidation;

namespace ServerApplicationLayer.Dtos.AuthDtos;
public sealed record RegisterDto ( string UserName, string Password, string ConfirmPassword );
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
  public RegisterDtoValidator ()
  {
    RuleFor (x => x.UserName).NotEmpty ();
    RuleFor (x => x.Password).NotEmpty ();
    RuleFor (x => x.ConfirmPassword).NotEmpty ();
    RuleFor (x => x.Password).Equal (x => x.ConfirmPassword);
  }
}
