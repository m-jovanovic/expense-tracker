using System;
using ExpenseTracker.Application.Abstractions;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace ExpenseTracker.Persistence.RavenDb
{
    /// <summary>
    /// Represents the document store provider.
    /// </summary>
    internal sealed class DocumentStoreProvider : IDocumentStoreProvider, IDisposable
    {
        private readonly DocumentStore _documentStore;

        public DocumentStoreProvider(IOptions<RavenDbSettings> options)
        {
            RavenDbSettings ravenDbSettings = options.Value;

            _documentStore = new DocumentStore
            {
                Urls = ravenDbSettings.Urls ?? throw new ArgumentException(nameof(RavenDbSettings.Urls)),
                Database = ravenDbSettings.Database ?? throw new ArgumentException(nameof(RavenDbSettings.Database))
            };

            _documentStore.Initialize();

            CreateDatabaseIfNotExists();
        }

        /// <inheritdoc />
        public IDocumentStore GetDocumentStore() => _documentStore;

        /// <inheritdoc />
        public void Dispose()
        {
            _documentStore?.Dispose();
        }

        /// <summary>
        /// Creates the database specified in the <see cref="IDocumentStore.Database"/> property if it doesn't exist.
        /// </summary>
        private void CreateDatabaseIfNotExists()
        {
            DatabaseRecordWithEtag databaseRecord = _documentStore.Maintenance.Server
                .Send(new GetDatabaseRecordOperation(_documentStore.Database));

            if (databaseRecord != null)
            {
                return;
            }

            var createDatabaseOperation = new CreateDatabaseOperation(new DatabaseRecord(_documentStore.Database));

            _documentStore.Maintenance.Server.Send(createDatabaseOperation);
        }
    }
}
