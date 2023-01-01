using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Contracts.Dtos.User;
using Rusty.Template.Contracts.Exceptions.Entity;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Contracts.Responses;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Attributes;
using Swashbuckle.AspNetCore.Annotations;

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
    ///     Gets all users
    /// </summary>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        return Ok(await _userRepo.GetAll().ProjectToType<UserDto>().ToListAsync(cancellationToken));
    }

    /// <summary>
    ///    Gets paged list with user dto
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(typeof(PagedResponse<UserDto>), StatusCodes.Status200OK)]
    [HttpPost("paged")]
    public async Task<IActionResult> GetPagedUsersAsync(OrderedPagedRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await _userRepo.PaginateAsync<UserDto>(request, cancellationToken));
    }

    /// <summary>
    ///     Gets user dto by id
    /// </summary>
    /// <param name="id">The id</param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundByIdException{User}"></exception>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetByIdAsync(id, cancellationToken) ??
                   throw new EntityNotFoundByIdException<User>(id);
        return Ok(user.Adapt<UserDto>());
    }

    /// <summary>
    ///     Gets user dto by username
    /// </summary>
    /// <param name="username">The username</param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundByNameException{User}"></exception>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [HttpGet("{username}")]
    public async Task<IActionResult> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetByUsernameAsync(username, cancellationToken) ??
                   throw new EntityNotFoundByNameException<User>(username);
        return Ok(user.Adapt<UserDto>());
    }

    /// <summary>
    ///     Gets the user update dto by id
    /// </summary>
    /// <param name="id">The id</param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundByIdException{User}"></exception>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(typeof(UserUpdateDto), StatusCodes.Status200OK)]
    [HttpGet("userToUpdate/{id:int}")]
    public async Task<IActionResult> GetUserToUpdateByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetByIdAsync(id, cancellationToken) ??
                   throw new EntityNotFoundByIdException<User>(id);
        return Ok(user.Adapt<UserUpdateDto>());
    }

    /// <summary>
    ///     Gets the user to update by name using the specified username
    /// </summary>
    /// <param name="username">The username</param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="EntityNotFoundByNameException{User}"></exception>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(typeof(UserUpdateDto), StatusCodes.Status200OK)]
    [HttpGet("userToUpdate/{username}")]
    public async Task<IActionResult> GetUserToUpdateByNameAsync(string username, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetByUsernameAsync(username, cancellationToken) ??
                   throw new EntityNotFoundByNameException<User>(username);
        return Ok(user.Adapt<UserUpdateDto>());
    }
    /// <summary>
    ///     Creates the user
    /// </summary>
    /// <param name="dto">The dto</param>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [SwaggerOperation(
        Summary = "Creates a new product",
        Description = "Requires admin privileges",
        OperationId = "CreateAsync",
        Tags = new[] { "Purchase", "Products" }
    )]
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(UserCreateDto dto)
    {
        var user = await _userRepo.CreateAsync(dto.Adapt<User>());
        return CreatedAtAction("GetUserById", new { id = user.Id }, user.Adapt<UserDto>());
    }

    /// <summary>
    ///     Updates the user
    /// </summary>
    /// <param name="id">The id</param>
    /// <param name="dto">The dto</param>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("{id:int}")]
    [HttpPutIdCompare]
    public async Task<IActionResult> UpdateUserAsync(int id, UserUpdateDto dto)
    {
        await _userRepo.UpdateAsync(dto.Adapt<User>());
        return NoContent();
    }

    /// <summary>
    ///     Deletes the user
    /// </summary>
    /// <param name="id">The id</param>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        await _userRepo.DeleteAsync(id);
        return NoContent();
    }
}