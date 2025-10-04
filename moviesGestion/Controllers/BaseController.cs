using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        // On utilise "protected" pour que les classes filles puissent y accéder
        protected readonly IMediator _mediator;

        protected ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
