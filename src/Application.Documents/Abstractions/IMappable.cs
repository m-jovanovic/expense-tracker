using AutoMapper;

namespace Application.Documents.Abstractions
{
    /// <summary>
    /// Represents an interface for a mappable class to define its mappings.
    /// </summary>
    public interface IMappable
    {
        /// <summary>
        /// Configures the mapping using the specified mapping profile.
        /// </summary>
        /// <param name="profile">The mapping profile.</param>
        void Mapping(Profile profile);
    }
}
