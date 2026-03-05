using Asp.Versioning;
using Azure.Core;
using E_learningPlatform.Application.Features.Payments.Commands.CreateCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration; 

    public PaymentsController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    [HttpPost("webhook/confirm-payment")]
    public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmPaymentCommand command)
    {
        // Security check using the Shared Secret
        if (!Request.Headers.TryGetValue("X-Payment-Secret", out var receivedSecret) ||
            receivedSecret != _configuration["PaymentSettings:WebhookSecret"])
        {
            return Unauthorized("Identity verification failed.");
        }

        var response = await _mediator.Send(command);
        return Ok(response);
    }
}