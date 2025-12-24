using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Queries;

public class GetActivityDetail
{
    public record Query(string Id) : IRequest<Activity?>;

    public class Handler(AppDbContext context) : IRequestHandler<Query, Activity?>
    {
        public async Task<Activity?> Handle(Query request, CancellationToken cancellationToken)
        {
            return await context.Activities.FindAsync(
                new object?[] { request.Id },
                cancellationToken
            );
        }
    }
}
