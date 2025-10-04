using MediatR;
using AutoMapper;
using moviesGestion.repositories;

namespace Application.Common.Commands
{
    public abstract class CreateCommandHandler<TCommand, TModel, TDto>
    : IRequestHandler<TCommand, int>
    where TCommand : IRequest<int>, ICreateCommand<TDto>
    where TModel : class
    where TDto : class
    {
        private readonly ITRepository<TModel> _repository;
        private readonly IMapper _mapper;

        protected CreateCommandHandler(ITRepository<TModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // La méthode Handle contient la logique réutilisable
        public virtual async Task<int> Handle(TCommand request, CancellationToken cancellationToken)
        {
            // 1. Mapper le DTO vers le modèle du domaine
            var entity = _mapper.Map<TModel>(request.Dto);

            // 2. Ajouter à la base de données
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            // 3. Retourner l'ID de la nouvelle entité
            // (Suppose que l'entité a une propriété "Id". Il faudra adapter si ce n'est pas le cas)
            var idProperty = entity.GetType().GetProperty("Id");
            if (idProperty == null)
            {
                throw new InvalidOperationException($"L'entité {typeof(TModel).Name} n'a pas de propriété 'Id'.");
            }

            return (int)idProperty.GetValue(entity);
        }
    }
}