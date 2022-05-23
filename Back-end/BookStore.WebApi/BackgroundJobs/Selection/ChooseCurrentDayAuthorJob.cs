using BookStore.Application.Commands.Selection.ChooseCurrentDayAuthor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.WebApi.BackgroundJobs.Selection;

internal class ChooseCurrentDayAuthorJob
{
    private IMediator Mediator { get; }

    public ChooseCurrentDayAuthorJob(IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task ChooseCurrentDayAuthor(CancellationToken cancellationToken)
    {
        await Mediator.Send(new ChooseCurrentDayAuthorCommand(), cancellationToken);
    }
}

