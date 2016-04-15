namespace CWI.BooMapper.Core.Relational
{
    internal struct ColumnMap
    {
        public ColumnMap(string readerName, string parsedName)
        {
            ReaderName = readerName;
            ParsedName = parsedName;
        }

        public string ReaderName { get; set; }

        public string ParsedName { get; set; }
    }
}
