using System.ComponentModel.DataAnnotations;
using MediatR;

public class GetUserByIdQuery : IRequest<GetUserByIdResponseDto> //รับค่า id และคืนค่าผลลัพธื GetUserByIdResponse ออก
{
    public string? Id { get; set; }
}
