using Api.Controllers;
using Application.Features.Movie.CreateMovie;
using MediatR;
using Microsoft.AspNetCore.Mvc;
public abstract class TController<TCreateCommand, TResponse> : ApiControllerBase
    where TCreateCommand : IRequest<TResponse> // Contrainte : TCreateCommand doit être une requête MediatR
{
    protected TController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<ActionResult<TResponse>> Create([FromBody] TCreateCommand command)
    {
        if (command == null)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);

        // On peut retourner le résultat directement. 
        // Pour un POST, il est souvent mieux de retourner un 201 Created.
        // Cela demanderait une convention (ex: le résultat est un objet avec une propriété Id).
        // Pour rester simple, nous retournons Ok(result).
        return Ok(result);
    }

    // On pourrait ajouter ici d'autres actions génériques pour GetById, Delete, etc.
}