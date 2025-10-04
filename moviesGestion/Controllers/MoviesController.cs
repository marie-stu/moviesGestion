using Api.Controllers;
using Application.Features.Movie.CreateMovie;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/movies")] // On spécifie la route concrète ici
public class MoviesController : ApiControllerBase
{
    public MoviesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMovieCommand command)
    {
        // 3. Envoyez la commande à MediatR
        // MediatR trouvera le bon handler (CreateMovieCommandHandler) et l'exécutera.
        var newMovieId = await _mediator.Send(command);

        // 4. Retournez une réponse HTTP appropriée
        // La meilleure pratique pour un POST qui crée une ressource est de retourner
        // un statut 201 Created avec une URL vers la nouvelle ressource.
        return CreatedAtAction(nameof(GetMovieById), new { id = newMovieId }, command);
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
        // TODO: Implémenter avec une GetMovieByIdQuery
        return Ok($"Récupération du film {id}");
    }
}

