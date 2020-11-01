using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZipCo.Users.Application.Requests.Accounts.Commands;
using ZipCo.Users.Application.Requests.Accounts.Queries;
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
    public class AccountController : ApiBaseController<AccountController>
    {
        /// <summary>
        /// Sign up an account for a member via member Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The new account</returns>
        /// <response code="200">Returns the new account</response>
        /// <response code="400">MemberId is not valid</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ApiSimpleResponse<AccountModel>> SignUp(SignUpAccountRequest request)
        {
            var command = Mapper.Map<SignUpAccountCommand>(request);
            var response = await Mediator.Send(command);
            return Mapper.Map<ApiSimpleResponse<AccountModel>>(response);
        }

        /// <summary>
        /// List accounts by some search conditions
        /// </summary>
        /// <returns>A list of accounts</returns>
        /// <response code="200">Returns a list of accounts</response>
        [HttpGet("list")]
        [ProducesResponseType(200)]
        public async Task<ApiPaginationResponse<AccountModel>> List([FromQuery]ListAccountsRequest request)
        {
            var query = Mapper.Map<ListAccountsQuery>(request);
            var response = await Mediator.Send(query);
            return Mapper.Map<ApiPaginationResponse<AccountModel>>(response);
        }
    }
}
