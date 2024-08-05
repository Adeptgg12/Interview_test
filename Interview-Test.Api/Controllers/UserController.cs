
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Interview_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    //Dependency Injection
    private readonly IMediator _mediator;
    
    //รับค่าเข้ามาและกำหนดให้กับฟิลด์  _mediator 
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    // httpget เป็นการกำหนดเส้น และ ประเภท
    [HttpGet("GetUserById")]
    public async Task<ActionResult> GetUserById([FromQuery] GetUserByIdQuery query) //รับข้อมูลผ่าน Query และ map ค่าลง GetUserByIdRequest
    {
        try
        {
            GetUserByIdResponseDto userReturn = await _mediator.Send(query); //เป็นการส่งค่าผ่าน mediator และรอรับผลลัพธ์กลับมา
            return Ok(userReturn);
        }
        catch (Exception e)
        {
            if (e is ValidationException)
            {
                throw;
            }

            return BadRequest(e);
        }

    }
    
    
    // httppost เป็นการกำหนดเส้น และ ประเภท
    [HttpPost("CreateUser")]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand command) //รับข้อมูลผ่าน Body และ map ค่าลง CreateUserCommand
    {
        try
        {
            CreateUserReq CreateUserReturn = await _mediator.Send(command); // ส่งค่าผ่าน Mediator และรอรับผลลัพธ์
            return Ok(CreateUserReturn);
        }
        catch (Exception e)
        {
            if (e is ValidationException)
            {
                throw;
            }
            return BadRequest(e);
        }
    

    }
}