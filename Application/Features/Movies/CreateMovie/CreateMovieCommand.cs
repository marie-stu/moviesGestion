using Application.Common.Commands;
using MediatR;

namespace Application.Features.Movie.CreateMovie
{    public class CreateMovieCommand : IRequest<int>, ICreateCommand<CreateMovieDto>
    {
        public CreateMovieDto Dto { get; }

        public CreateMovieCommand(CreateMovieDto dto)
        {
            Dto = dto;
        }
    }
}
