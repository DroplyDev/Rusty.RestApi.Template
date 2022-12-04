using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rusty.Template.Application.Repositories;
using Rusty.Template.Contracts.Dtos.WeatherForecast;
using Rusty.Template.Contracts.Exceptions.Entity;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Domain;

namespace Rusty.Template.Presentation.Controllers;

[Route("[controller]")]
public class WeatherForecastsController : BaseApiController
{
    private readonly IWeatherForecastRepo _weatherForecastRepo;

    public WeatherForecastsController(IWeatherForecastRepo weatherForecastRepo)
    {
        _weatherForecastRepo = weatherForecastRepo;
    }

    // GET: api/WeatherForecast
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeatherForecastDto>>> GetWeatherForecasts(PagedInfoRequest request)
    {
        return await _weatherForecastRepo.GetAll().ProjectToType<WeatherForecastDto>().ToListAsync();
    }

    // GET: api/WeatherForecast/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WeatherForecastDto>> GetWeatherForecast(int id)
    {
        var weatherForecast = await _weatherForecastRepo.GetByIdAsync(id);

        if (weatherForecast is null)
            throw new EntityNotFoundByIdException<WeatherForecast>(id);

        return weatherForecast.Adapt<WeatherForecastDto>();
    }

    // PUT: api/WeatherForecast/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWeatherForecast(WeatherForecastUpdateDto weatherForecast)
    {
        // if (await _weatherForecastRepo.ExistsAsync(id)) throw new EntityAlre

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

    // POST: api/WeatherForecast
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WeatherForecastDto>> PostWeatherForecast(WeatherForecastCreateDto weatherForecast)
    {
        var createdEntity = await _weatherForecastRepo.CreateAsync(weatherForecast.Adapt<WeatherForecast>());
        return CreatedAtAction(nameof(GetWeatherForecast), new { id = createdEntity.Id },
            createdEntity.Adapt<WeatherForecastDto>());
    }

    // DELETE: api/WeatherForecast/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWeatherForecast(int id)
    {
        await _weatherForecastRepo.DeleteAsync(id);
        return NoContent();
    }
}