using AutoMapper;
using Interview_Test.Repositories.Interfaces;
using MediatR;


public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponseDto> //จัดการกับคำขอของ GetUserByIdQuery คืนข้อมูลของ GetUserByIdResponse
{
    private readonly IUserRepository _userRepository;
    
    private readonly IMapper _autoMapper;
    
    //รับค่าเข้ามาและกำหนดให้กับฟิลด์  _userRepository
    public GetUserByIdHandler(IUserRepository userRepository, IMapper autoMapper)
    {
        _userRepository = userRepository;
        _autoMapper = autoMapper;
    }

    public async Task<GetUserByIdResponseDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken) //Handle จัดการคำขอ (query) ที่ส่งเข้ามา
    { 
        
        var user =  _userRepository.GetUserById(query.Id);
        GetUserByIdResponseDto dto = new GetUserByIdResponseDto();
        _autoMapper.Map(user, dto);
        return dto;
    }
}