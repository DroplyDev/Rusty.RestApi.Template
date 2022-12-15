using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Contracts.Dtos.WeatherForecast;
using Rusty.Template.Contracts.Exceptions.Entity;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Attributes;

namespace Rusty.Template.Presentation.Controllers.V2;

/// <summary>
///     Weather Forecast controller
/// </summary>
[ApiVersion("2.0")]
public class WeatherForecastsController : BaseApiController
{
    private readonly IWeatherForecastRepo _weatherForecastRepo;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="weatherForecastRepo"></param>
    public WeatherForecastsController(IWeatherForecastRepo weatherForecastRepo)
    {
        _weatherForecastRepo = weatherForecastRepo;
    }

    /// <summary>
    ///     Gets paged order list of weather forecasts
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("paged")]
    public async Task<IActionResult> GetWeatherForecasts(OrderByPagedRequest request)
    {
        return Ok(await _weatherForecastRepo.PaginateAsync<WeatherForecastDto>(request));
    }

    /// <summary>
    ///     Gets weather forecast by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundByIdException{WeatherForecast}"></exception>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetWeatherForecast(int id)
    {
        var weatherForecast = await _weatherForecastRepo.GetByIdAsync(id);

        if (weatherForecast is null)
            throw new EntityNotFoundByIdException<WeatherForecast>(id);

        return Ok(weatherForecast.Adapt<WeatherForecastDto>());
    }

    /// <summary>
    ///     Updates weather forecast
    /// </summary>
    /// <param name="id"></param>
    /// <param name="weatherForecast"></param>
    /// <returns></returns>
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
    ///     Creates weather forecast
    /// </summary>
    /// <param name="weatherForecast"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostWeatherForecast(WeatherForecastCreateDto weatherForecast)
    {
        var createdEntity = await _weatherForecastRepo.CreateAsync(weatherForecast.Adapt<WeatherForecast>());
        return CreatedAtAction(nameof(GetWeatherForecast), new { id = createdEntity.Id },
            createdEntity.Adapt<WeatherForecastDto>());
    }

    /// <summary>
    ///     Deletes weather forecast
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteWeatherForecast(int id)
    {
        await _weatherForecastRepo.DeleteAsync(id);
        return NoContent();
    }
}