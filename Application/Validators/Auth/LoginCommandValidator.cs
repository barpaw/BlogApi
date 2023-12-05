using BlogApi.Application.Commands;
using FluentValidation;

namespace BlogApi.Application.Validators.Auth;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(loginDto => loginDto.Username).NotEmpty().WithMessage("Your username cannot be empty");
        RuleFor(loginDto => loginDto.Username).Length(3, 50).WithMessage("Your username must be between 1 and 50 characters.");
            
        
        RuleFor(loginDto => loginDto.Password).NotEmpty().WithMessage("Your password cannot be empty");
        RuleFor(loginDto => loginDto.Password).Length(8, 50).WithMessage("Your password must be between 8 and 100 characters.");
        
    }
}