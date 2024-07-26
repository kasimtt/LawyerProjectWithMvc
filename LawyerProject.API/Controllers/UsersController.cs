using LawyerProject.Application.CustomAttributes;
using LawyerProject.Application.Enums;
using LawyerProject.Application.Features.Commands.AppUsers.AssignRoleToUser;
using LawyerProject.Application.Features.Commands.AppUsers.CreateUser;
using LawyerProject.Application.Features.Commands.AppUsers.GoogleLogin;
using LawyerProject.Application.Features.Commands.AppUsers.LoginUser;
using LawyerProject.Application.Features.Commands.AppUsers.PasswordUpdate;
using LawyerProject.Application.Features.Commands.AppUsers.UpdateUser;
using LawyerProject.Application.Features.Queries.AppUser.GetAllUsers;
using LawyerProject.Application.Features.Queries.AppUser.GetRolesToUser;
using LawyerProject.Application.Features.Queries.AppUser.GetRolesToUserNameOrEmail;
using LawyerProject.Application.Features.Queries.AppUser.GetUserByUserName;
using LawyerProject.Application.Features.Queries.AppUser.GetUserForProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LawyerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")] // action'u client isterse kaldırız
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommadRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            if (response.Success)
            {
                return Ok(response);
            }
            return Ok(response);
        }

        [HttpGet("[action]/{UserNameOrEmail}")]
        public async Task<IActionResult> GetUserByUserName([FromRoute] GetUserByUserNameQueryRequest request)
        {
            GetUserByUserNameQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword2([FromBody] PasswordUpdateCommandRequest request)
        {
            PasswordUpdateCommandResponse response = await _mediator.Send(request);
            return Ok();
        }

        [HttpGet("[action]/{UserNameOrEmail}")]
        public async Task<IActionResult> GetUserDetailsByUserName([FromRoute] GetUserDetailsByUserEmailQueryRequest request)
        {
            GetUserDetailsByUserEmailQueryResponse response = await _mediator.Send(request);
            return Ok(response.User);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest request)
        {
            UpdateUserCommandResponse response = await _mediator.Send(request);
            return Ok();
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Users", Menu = "Users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryReqeuest request)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Assign Role To User", Menu = "Users")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserCommandRequest request)
        {
            AssignRoleToUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }


        [HttpGet("get-roles-to-user/{UserID}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles To User", Menu = "Users")]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest request)
        {
            GetRolesToUserQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("get-roles-to-usernameoremail/{UserNameOrEmail}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles To UserNameOrEmail", Menu = "Users")]
        public async Task<IActionResult> GetRolesToUserNameOrEmail([FromRoute] GetRolesToUserNameOrEmailQueryRequest request)
        {
            GetRolesToUserNameOrEmailQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
