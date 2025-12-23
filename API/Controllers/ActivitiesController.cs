using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class ActivitiesController(AppDbContext context) : BaseApiController
{
   // private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await context.Activities.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivityDetail(string id)
    {
        var activity = await context.Activities.FindAsync(id);
        if (activity is null) return NotFound();

        return activity;
    }

    [HttpPost]
    public async Task<ActionResult<Activity>> CreateActivity(Activity activity)
    {
        context.Activities.Add(activity);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetActivityDetail), new { id = activity.Id }, activity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(string id, Activity updated)
    {
        if (id != updated.Id) return BadRequest("El id de la ruta no coincide con el cuerpo.");

        var exists = await context.Activities.AnyAsync(a => a.Id == id);
        if (!exists) return NotFound();

        context.Entry(updated).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(string id)
    {
        var activity = await context.Activities.FindAsync(id);
        if (activity is null) return NotFound();

        context.Activities.Remove(activity);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
