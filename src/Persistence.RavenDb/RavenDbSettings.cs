namespace Persistence.RavenDb
{
    /// <summary>
    /// Represents the RavenDb settings.
    /// </summary>
    internal sealed class RavenDbSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RavenDbSettings"/> class.
        /// </summary>
        public RavenDbSettings()
        {
            Database = string.Empty;
        }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string[] Urls { get; set; }

        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        public string Database { get; set; }
    }
}
