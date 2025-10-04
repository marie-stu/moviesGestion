using Api.Controllers;
using Application.Features.Movie.CreateMovie;
using MediatR;
using Microsoft.AspNetCore.Mvc;
public abstract class TController<TCreateCommand, TResponse> : ApiControllerBase
    where TCreateCommand : IRequest<TResponse> // Contrainte : TCreateCommand doit �tre une requ�te MediatR
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

        // On peut retourner le r�sultat directement. 
        // Pour un POST, il est souvent mieux de retourner un 201 Created.
        // Cela demanderait une convention (ex: le r�sultat est un objet avec une propri�t� Id).
        // Pour rester simple, nous retournons Ok(result).
        return Ok(result);
    }

    // On pourrait ajouter ici d'autres actions g�n�riques pour GetById, Delete, etc.
}