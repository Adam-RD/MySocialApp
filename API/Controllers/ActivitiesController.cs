using Application.Activities.Commands;
using Application.Activities.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await Mediator.Send(new GetActivityList.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivityDetail(string id)
    {
        var activity = await Mediator.Send(new GetActivityDetail.Query(id));
        if (activity is null) return NotFound();

        return activity;
    }

    [HttpPost]
    public async Task<ActionResult<Activity>> CreateActivity(Activity activity)
    {
        var created = await Mediator.Send(new CreateActivity.Command(activity));

        return CreatedAtAction(nameof(GetActivityDetail), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity(string id, Activity updated)
    {
        if (id != updated.Id) return BadRequest("El id de la ruta no coincide con el cuerpo.");

        var updatedResult = await Mediator.Send(new EditActivity.Command(updated));
        if (!updatedResult) return NotFound();

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> EditActivity(Activity updated)
    {
        if (string.IsNullOrWhiteSpace(updated.Id))
            return BadRequest("El id es obligatorio en el cuerpo.");

        var updatedResult = await Mediator.Send(new EditActivity.Command(updated));
        if (!updatedResult) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(string id)
    {
        var deleted = await Mediator.Send(new DeleteActivity.Command(id));
        if (!deleted) return NotFound();

        return NoContent();
    }
}
