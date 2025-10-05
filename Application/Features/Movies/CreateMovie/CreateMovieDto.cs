using Application.auMapper;

namespace Application.Features.Movie.CreateMovie
{
    public class CreateMovieDto : IMapFrom<Media>, IMapTo<Media>
    {
        public string Title { get; set; }
        public int? ReleaseYear { get; set; }
        public int? GenreId { get; set; }


    }
}
