namespace CWI.BooMapper.Core.Oto
{
    public delegate TOut OtoMapper<in TIn, out TOut>(TIn target) where TIn : class
                                                                 where TOut : class;
}
