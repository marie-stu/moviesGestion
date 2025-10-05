using MediatR;
using AutoMapper;
using moviesGestion.repositories;
using System.Reflection;

namespace Application.Common.Commands
{
    /// <summary>
    /// Handler de base pour les commandes de création.
    /// Peut être étendu pour ajouter des règles métier spécifiques.
    /// </summary>
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

        public virtual async Task<int> Handle(TCommand request, CancellationToken cancellationToken)
        {
            // 1. Mapper le DTO vers le modèle du domaine
            var entity = _mapper.Map<TModel>(request.Dto);

            // 2. Appliquer les règles métier (méthode virtuelle pour permettre l'override)
            await ApplyBusinessRules(entity, request);

            // 3. Valider l'entité (optionnel)
            await ValidateEntity(entity);

            // 4. Ajouter à la base de données
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            // 5. Retourner l'ID de la nouvelle entité
            return GetEntityId(entity);
        }

        /// <summary>
        /// Méthode virtuelle pour appliquer des règles métier spécifiques.
        /// À surcharger dans les classes dérivées si nécessaire.
        /// </summary>
        protected virtual Task ApplyBusinessRules(TModel entity, TCommand request)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Méthode virtuelle pour valider l'entité.
        /// À surcharger dans les classes dérivées si nécessaire.
        /// </summary>
        protected virtual Task ValidateEntity(TModel entity)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Récupère l'ID de l'entité créée.
        /// Cherche une propriété nommée "Id", ou se terminant par "Id".
        /// </summary>
        private int GetEntityId(TModel entity)
        {
            var type = typeof(TModel);

            // Cherche d'abord une propriété nommée exactement "Id"
            var idProperty = type.GetProperty("Id");

            // Si pas trouvé, cherche la première propriété se terminant par "Id"
            if (idProperty == null)
            {
                idProperty = type.GetProperties()
                    .FirstOrDefault(p => p.Name.EndsWith("Id") && p.PropertyType == typeof(int));
            }

            if (idProperty == null)
            {
                throw new InvalidOperationException(
                    $"L'entité {type.Name} n'a pas de propriété 'Id' ou se terminant par 'Id'.");
            }

            var id = idProperty.GetValue(entity);

            if (id == null)
            {
                throw new InvalidOperationException(
                    $"La propriété {idProperty.Name} de l'entité {type.Name} est null.");
            }

            return (int)id;
        }
    }
}