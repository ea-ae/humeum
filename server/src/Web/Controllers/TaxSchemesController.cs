using Application.TaxSchemes.Queries;
using Application.TaxSchemes.Queries.GetTaxScheme;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Filters;

namespace Web.Controllers;

/// <inheritdoc cref="Domain.TaxSchemeAggregate.TaxScheme"/>
[Route("api/v1/[controller]")]
[ApplicationExceptionFilter]
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
    public async Task<ActionResult<IEnumerable<TaxSchemeDto>>> GetTaxSchemes(GetTaxSchemesQuery query) {
        var taxSchemes = await _mediator.Send(query);
        return Ok(taxSchemes);
    }
}
