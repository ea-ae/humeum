using Application.Users.Queries;
using Application.V1.TaxSchemes.Queries;
using MediatR;

using Microsoft.AspNetCore.Mvc;

using Shared.Interfaces;

using Web.Filters;

namespace Web.Controllers;

/// <inheritdoc cref="Domain.V1.TaxSchemeAggregate.TaxScheme"/>
[Route("api/v{Version:ApiVersion}/[controller]")]
[ApplicationResultFilter]
[CsrfXHeaderFilter]
[Produces("application/json")]
[ApiController]
public class TaxSchemesController : ControllerBase {
    private readonly IMediator _mediator;

    /// <summary>Initializes a new controller.</summary>
    public TaxSchemesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Get all tax schemes.
    /// </summary>
    /// <response code="200">List of tax schemes.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaxSchemeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IResult<IEnumerable<TaxSchemeDto>, IBaseException>>> GetTaxSchemes(GetTaxSchemesQuery query) {
        var taxSchemes = await _mediator.Send(query);
        return Ok(taxSchemes);
    }
}
