using Microsoft.AspNetCore.Mvc;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Contracts.Dtos;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Contracts.Responses;

namespace Rusty.Template.Presentation.Controllers.V1;

/// <summary>
///     The users controller class
/// </summary>
/// <seealso cref="BaseApiController" />
[ApiVersion("1.0", Deprecated = false)]
public class UsersController : BaseApiController
{
    /// <summary>
    ///     The user repo
    /// </summary>
    private readonly IUserRepo _userRepo;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UsersController" /> class
    /// </summary>
    /// <param name="userRepo">The user repo</param>
    public UsersController(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    /// <summary>
    ///     Paginates the request
    /// </summary>
    /// <param name="request">The request</param>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(typeof(PagedResponse<UserDto>), StatusCodes.Status200OK)]
    [HttpPost("Paged")]
    public async Task<IActionResult> PaginateAsync(OrderedPagedRequest request)
    {
        return Ok(await _userRepo.PaginateAsync<UserDto>(request));
    }
}