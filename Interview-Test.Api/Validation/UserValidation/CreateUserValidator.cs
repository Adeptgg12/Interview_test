using FluentValidation;
using MediatR;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x).Custom((property, action) =>
        {
            var messages = new List<string>();
            if (string.IsNullOrEmpty(property.Username?.ToString()))
            {
                messages.Add("Username is required!");
            }
            if (string.IsNullOrEmpty(property.FirstName?.ToString()))
            {
                messages.Add("FirstName is required!");
            }
 
            if (string.IsNullOrEmpty(property.LastName?.ToString()))
            {
                messages.Add("LastName is required!");
            }
            if (messages.Any())
            {
                action.AddFailure(new FluentValidation.Results.ValidationFailure
                {
                    ErrorCode = "1",
                    ErrorMessage = String.Join(", ", messages)
                });
            }
            
            /*if (string.IsNullOrEmpty(property.Username))
            {
                action.AddFailure(new FluentValidation.Results.ValidationFailure
                {
                    ErrorCode = "0",
                    ErrorMessage = "Username is required!"
                });
            }
            if (string.IsNullOrEmpty(property.FirstName))
            {
                action.AddFailure(new FluentValidation.Results.ValidationFailure
                {
                    ErrorCode = "0",
                    ErrorMessage = "FirstName is required!"
                });
            }
            if (string.IsNullOrEmpty(property.LastName))
            {
                action.AddFailure(new FluentValidation.Results.ValidationFailure
                {
                    ErrorCode = "0",
                    ErrorMessage = "LastName is required!"
                });
            }*/
        });
    }
}