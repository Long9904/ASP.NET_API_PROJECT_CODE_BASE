using Application.Common.Errors;
using Application.Common.Results;
using Application.Features.UserFeture;
using Domain.Entities;
using Domain.Interface.IRepository;
using FluentValidation;
using MediatR;

namespace Application.Features.UserFeature
{
    // I don't build structures like Auth, because if we build a simple feature like User, we don't need to build complex structures like Auth.

    // If you don't like it, you can devide it into structures like Auth.

    /*
     User
        |-- CreateUser
        |-- GetUser
        |-- UpdateUser
        |-- DeleteUser
        |-- UserDto
     */

    //----------------------------------------------------------------------------------------------
    // 1. Build CreateUser Command
    // IRequest<TResponse> is from MediatR library, it represents a request with a response of type TResponse. --> simple mean: This command will return a  UserDto when handled.

    public record CreateUserCommand(
        string Username,
        string Email,
        string Password) : IRequest<Result<UserDto>>;


    //----------------------------------------------------------------------------------------------
    // 2. Build Validator for CreateUser Command

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }


    //----------------------------------------------------------------------------------------------
    // 3. Build Handler for CreateUser Command
    // In this class, we also use Result<T> pattern to return either a UserDto or an error.
    // By using MediatR, we don't need add dependency injection for this handler, MediatR will handle it for us.

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            // Check if email already exists
            var existingUser = await _userRepository.ExistsAsync(u => u.Email == request.Email, cancellationToken);

            // The error is defined in Application/Common/Errors/UserErrors.cs
            if (existingUser)
            {
                return Result<UserDto>.Failure(UserErrors.EmailAlreadyExists);
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password // consider using a hashing method
            };

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // You can use AutoMapper here if you have many properties to map
            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            // Return success by wrapping the UserDto in a Result<UserDto>
            return Result<UserDto>.Success(userDto);
        }
    }

}
