using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Commands;

public class EditActivity
{
    public record Command(Activity Activity) : IRequest<bool>;

    public class Handler(AppDbContext context) : IRequestHandler<Command, bool>
    {
        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var exists = await context.Activities.AnyAsync(
                a => a.Id == request.Activity.Id,
                cancellationToken
            );

            if (!exists) return false;

            context.Entry(request.Activity).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
