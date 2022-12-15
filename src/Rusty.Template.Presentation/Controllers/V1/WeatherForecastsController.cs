using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Contracts.Dtos.WeatherForecast;
using Rusty.Template.Contracts.Exceptions.Entity;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Attributes;

namespace Rusty.Template.Presentation.Controllers.V1;

/// <summary>
///     The weather forecasts controller class
/// </summary>
/// <seealso cref="BaseApiController" />
[ApiVersion("1.0", Deprecated = true)]
public class WeatherForecastsController : BaseApiController
{
    /// <summary>
    ///     The weather forecast repo
    /// </summary>
    private readonly IWeatherForecastRepo _weatherForecastRepo;

    /// <summary>
    ///     Initializes a new instance of the <see cref="WeatherForecastsController" /> class
    /// </summary>
    /// <param name="weatherForecastRepo">The weather forecast repo</param>
    public WeatherForecastsController(IWeatherForecastRepo weatherForecastRepo)
    {
        _weatherForecastRepo = weatherForecastRepo;
    }

    /// <summary>
    ///     Gets the weather forecasts using the specified request
    /// </summary>
    /// <param name="request">The request</param>
    /// <returns>A task containing the action result</returns>
    [HttpPost("paged")]
    public async Task<IActionResult> GetWeatherForecasts(OrderByPagedRequest request)
    {
        return Ok(await _weatherForecastRepo.PaginateAsync<WeatherForecastDto>(request));
    }

    /// <summary>
    ///     Gets the weather forecast using the specified id
    /// </summary>
    /// <param name="id">The id</param>
    /// <exception cref="EntityNotFoundByIdException{WeatherForecast}"></exception>
    /// <returns>A task containing the action result</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetWeatherForecast(int id)
    {
        var weatherForecast = await _weatherForecastRepo.GetByIdAsync(id);

        if (weatherForecast is null)
            throw new EntityNotFoundByIdException<WeatherForecast>(id);

        return Ok(weatherForecast.Adapt<WeatherForecastDto>());
    }

    /// <summary>
    ///     Puts the weather forecast using the specified id
    /// </summary>
    /// <param name="id">The id</param>
    /// <param name="weatherForecast">The weather forecast</param>
    /// <returns>A task containing the action result</returns>
    [HttpPut("{id:int}")]
    [HttpPutIdCompare]
    public async Task<IActionResult> PutWeatherForecast(int id, WeatherForecastUpdateDto weatherForecast)
    {
        try
        {
            await _weatherForecastRepo.UpdateAsync(weatherForecast.Adapt<WeatherForecast>());
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
            throw;
        }

        return NoContent();
    }

    /// <summary>
    ///     Posts the weather forecast using the specified weather forecast
    /// </summary>
    /// <param name="weatherForecast">The weather forecast</param>
    /// <returns>A task containing the action result</returns>
    [HttpPost]
    public async Task<IActionResult> PostWeatherForecast(WeatherForecastCreateDto weatherForecast)
    {
        var createdEntity = await _weatherForecastRepo.CreateAsync(weatherForecast.Adapt<WeatherForecast>());
        return CreatedAtAction(nameof(GetWeatherForecast), new { id = createdEntity.Id },
            createdEntity.Adapt<WeatherForecastDto>());
    }

    /// <summary>
    ///     Deletes the weather forecast using the specified id
    /// </summary>
    /// <param name="id">The id</param>
    /// <returns>A task containing the action result</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteWeatherForecast(int id)
    {
        await _weatherForecastRepo.DeleteAsync(id);
        return NoContent();
    }
}