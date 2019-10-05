using AutoMapper;

namespace ExpenseTracker.Application.Abstractions
{
    internal interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
