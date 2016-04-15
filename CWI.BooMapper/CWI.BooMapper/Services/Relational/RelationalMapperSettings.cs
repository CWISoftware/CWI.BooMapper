namespace CWI.BooMapper.Services.Relational
{
    /// <summary>
    /// Provides configuration settings for a <see cref="IRelationalMapperService"/>
    /// </summary>
    public class RelationalMapperSettings
    {
        /// <summary>
        /// Gets or Sets a value indicating that the DataReader instance should be disposed.
        /// </summary>
        public bool DisposeReader { get; set; }
    }
}
