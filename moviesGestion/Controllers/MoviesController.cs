using Api.Controllers;
using Application.Features.Movie.CreateMovie;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/movies")] // On sp�cifie la route concr�te ici
public class MoviesController : ApiControllerBase
{
    public MoviesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMovieCommand command)
    {
        // 3. Envoyez la commande � MediatR
        // MediatR trouvera le bon handler (CreateMovieCommandHandler) et l'ex�cutera.
        var newMovieId = await _mediator.Send(command);

        // 4. Retournez une r�ponse HTTP appropri�e
        // La meilleure pratique pour un POST qui cr�e une ressource est de retourner
        // un statut 201 Created avec une URL vers la nouvelle ressource.
        return CreatedAtAction(nameof(GetMovieById), new { id = newMovieId }, command);
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
        // TODO: Impl�menter avec une GetMovieByIdQuery
        return Ok($"R�cup�ration du film {id}");
    }
}

