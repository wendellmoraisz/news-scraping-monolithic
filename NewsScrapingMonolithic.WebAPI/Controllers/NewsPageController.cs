using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsScrapingMonolithic.Application.UseCases.CreateNewsPage;

namespace NewsScrapingMonolithic.WebAPI.Controllers;

[ApiController]
[Route("newspages")]
public class NewsPageController : ControllerBase
{
    private readonly IMediator _mediator;

    public NewsPageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateNewsPageResponse>> Create(CreateNewsPageRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Created("GetByUrl", response);
    }
}