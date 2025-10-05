using AutoMapper;

namespace Application.auMapper
{
    public interface IMapTo<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
