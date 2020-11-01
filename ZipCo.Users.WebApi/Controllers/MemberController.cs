using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZipCo.Users.Application.Requests.Members.Commands;
using ZipCo.Users.Application.Requests.Members.Queries;
using ZipCo.Users.WebApi.Models;
using ZipCo.Users.WebApi.Requests;
using ZipCo.Users.WebApi.Responses;

namespace ZipCo.Users.WebApi.Controllers
{
    /// <summary>
    /// Endpoint for query and modify account resource 
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    public class MemberController : ApiBaseController<MemberController>
    {
        /// <summary>
        /// Sign up an account for a member via member Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The new account</returns>
        /// <response code="200">Create the new account item successfully</response>
        /// <response code="400">MemberId is invalid</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ApiSimpleResponse<MemberModel>> SignUp(SignUpMemberRequest request)
        {
            var command = Mapper.Map<SignUpMemberCommand>(request);
            var response = await Mediator.Send(command);
            return Mapper.Map<ApiSimpleResponse<MemberModel>>(response);
        }

        /// <summary>
        /// Get member details via member email address
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>A member item</returns>
        /// <response code="200">Returns the member item successfully</response>
        /// <response code="400">Member email is invalid</response>
        /// <response code="404">Member is not found from email</response>
        [HttpGet("{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ApiSimpleResponse<MemberModel>> GetMemberByEmail([FromRoute]string email)
        {
            var query = Mapper.Map<GetMemberByEmailQuery>(email);
            var response = await Mediator.Send(query);
            return Mapper.Map<ApiSimpleResponse<MemberModel>>(response);
        }

        /// <summary>
        /// List members by some search conditions
        /// </summary>
        /// <returns>A list of members</returns>
        /// <response code="200">Returns a list of members</response>
        [HttpGet("list")]
        [ProducesResponseType(200)]
        public async Task<ApiPaginationResponse<MemberModel>> List([FromQuery]ListMemberRequest request)
        {
            var query = Mapper.Map<ListMembersQuery>(request);
            var response = await Mediator.Send(query);
            return Mapper.Map<ApiPaginationResponse<MemberModel>>(response);
        }

    }
}
