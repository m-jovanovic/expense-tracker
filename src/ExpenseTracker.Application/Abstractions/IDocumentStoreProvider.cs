using Raven.Client.Documents;

namespace ExpenseTracker.Application.Abstractions
{
    /// <summary>
    /// Represents a document store provider interface.
    /// </summary>
    public interface IDocumentStoreProvider
    {
        /// <summary>
        /// Gets the document store instance.
        /// </summary>
        /// <returns>The configured and initialized document store instance.</returns>
        IDocumentStore GetDocumentStore();
    }
}
