namespace CWI.BooMapper.Core.Relational
{
    internal struct ColumnMap
    {
        public ColumnMap(string readerName, string parsedName)
        {
            ReaderName = readerName;
            ParsedName = parsedName;
        }

        public string ReaderName { get; private set; }

        public string ParsedName { get; private set; }
    }
}
