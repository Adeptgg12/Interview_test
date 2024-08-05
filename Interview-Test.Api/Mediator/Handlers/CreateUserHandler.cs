using AutoMapper;
using Interview_Test.Repositories.Interfaces;
using MediatR;


public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserReq>
{
    private readonly IUserRepository _userRepository;
    
    private readonly IMapper _autoMapper;

    //รับค่าเข้ามาและกำหนดให้กับฟิลด์  _userRepository
    public CreateUserHandler(IUserRepository userRepository, IMapper autoMapper)
    {
        _userRepository = userRepository;
        _autoMapper = autoMapper;
    }
    
    public async Task<CreateUserReq> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        
        CreateUserReq user = new CreateUserReq();
        _autoMapper.Map(command, user);
        var createUser = _userRepository.CreateUser(user);
        
        return user;
    }
}