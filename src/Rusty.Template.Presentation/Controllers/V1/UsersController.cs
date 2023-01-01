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
    ///     Gets the all
    /// </summary>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _userRepo.GetAll().ProjectToType<UserDto>().ToListAsync());
    }

    /// <summary>
    ///     Paginates list
    /// </summary>
    /// <param name="request">The request</param>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(typeof(PagedResponse<UserDto>), StatusCodes.Status200OK)]
    [HttpPost("Paged")]
    public async Task<IActionResult> PaginateAsync(OrderedPagedRequest request)
    {
        return Ok(await _userRepo.PaginateAsync<UserDto>(request));
    }

    /// <summary>
    ///     Gets user by id
    /// </summary>
    /// <param name="id">The id</param>
    /// <exception cref="EntityNotFoundByIdException{User}"></exception>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var user = await _userRepo.GetByIdAsync(id) ?? throw new EntityNotFoundByIdException<User>(id);
        return Ok(user.Adapt<UserDto>());
    }

    /// <summary>
    ///     Gets user by username
    /// </summary>
    /// <param name="username">The username</param>
    /// <exception cref="EntityNotFoundByNameException{User}"></exception>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUsernameAsync(string username)
    {
        var user = await _userRepo.GetByUsernameAsync(username) ??
                   throw new EntityNotFoundByNameException<User>(username);
        return Ok(user.Adapt<UserDto>());
    }

    /// <summary>
    ///     Creates the user
    /// </summary>
    /// <param name="dto">The dto</param>
    /// <returns>A task containing the action result</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserCreateDto dto)
    {
        var user = await _userRepo.CreateAsync(dto.Adapt<User>());
        return CreatedAtAction("GetById", new { id = user.Id }, user.Adapt<UserDto>());
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
    public async Task<IActionResult> UpdateAsync(int id, UserUpdateDto dto)
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
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _userRepo.DeleteAsync(id);
        return NoContent();
    }
}