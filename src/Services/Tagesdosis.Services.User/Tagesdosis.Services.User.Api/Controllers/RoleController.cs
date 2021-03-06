using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Services.User.Commands.Role.AddRoleToUserCommand;
using Tagesdosis.Services.User.Queries.Role.GetRolesForUser;

namespace Tagesdosis.Services.User.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
    public async Task<IActionResult> GetRolesForUserAsync()
    {
        var query = new GetRolesForUserQuery {UserName = User.Identity!.Name!};
        var response = await _mediator.Send(query);

        if (response.IsValid)
            return Ok(response);
        
        return NotFound(response);
    }
    
    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public async Task<IActionResult> AddRoleToUserAsync([FromQuery] string role)
    {
        var command = new AddRoleToUserCommand()
        {
            UserName = User.Identity!.Name,
            Role = role
        };

        var response = await _mediator.Send(command);
        if (response.IsValid)
            return Ok(response);
        
        return BadRequest(response);
    }
}