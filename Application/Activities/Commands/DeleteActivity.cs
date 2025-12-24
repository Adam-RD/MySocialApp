using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class DeleteActivity
{
    public record Command(string Id) : IRequest<bool>;

    public class Handler(AppDbContext context) : IRequestHandler<Command, bool>
    {
        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync(request.Id, cancellationToken);
            if (activity is null) return false;

            context.Activities.Remove(activity);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
