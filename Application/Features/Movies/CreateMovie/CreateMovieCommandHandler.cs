using Application.Common.Commands;
using AutoMapper;
using MediatR;
using moviesGestion.repositories;

namespace Application.Features.Movie.CreateMovie
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
    {
        private readonly ITRepository<Media> _repository;
        private readonly IMapper _mapper;

        // ... constructeur

        public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            // 1. Mapper les données venant de l'utilisateur
            var mediaEntity = _mapper.Map<Media>(request); // Ou request.Dto selon votre design

            // 2. Appliquer les règles métier / valeurs par défaut
            mediaEntity.MediaType = "movie"; 

            // 3. Sauvegarder en base de données
            await _repository.AddAsync(mediaEntity);
            await _repository.SaveChangesAsync();


            return mediaEntity.MediaId;
        }
    }
}
