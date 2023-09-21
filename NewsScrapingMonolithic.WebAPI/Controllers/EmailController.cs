using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

namespace NewsScrapingMonolithic.WebAPI.Controllers;

[ApiController]
[Route("emails")]
public class EmailController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmailController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateEmailAddressResponse>> Create(CreateEmailAddressRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request,cancellationToken);
        return Created("GetByAddress",new { id = response.Id });
    }
}