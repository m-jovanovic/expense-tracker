using AutoMapper;

namespace Application.Abstractions
{
    /// <summary>
    /// Represents an interface for a mappable class to define its mappings.
    /// </summary>
    internal interface IMappable
    {
        void Mapping(Profile profile);
    }
}
