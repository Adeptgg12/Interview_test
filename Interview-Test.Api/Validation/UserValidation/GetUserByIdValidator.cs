
using FluentValidation;
using Interview_Test.Repositories.Interfaces;

public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        RuleFor(x => x).Custom((propaty, action) =>
        {
            if (string.IsNullOrEmpty(propaty.Id))
            {
                action.AddFailure(new FluentValidation.Results.ValidationFailure
                {
                    ErrorCode = "1",
                    ErrorMessage = "Id is required"
                });
            }
            var userExitUserById = _userRepository.ExitUserById(propaty.Id);
            if (userExitUserById == false)
            {
                action.AddFailure(new FluentValidation.Results.ValidationFailure
                {
                    ErrorCode = "999",
                    ErrorMessage = "User Not Found"
                });
            }
        });
    }
}