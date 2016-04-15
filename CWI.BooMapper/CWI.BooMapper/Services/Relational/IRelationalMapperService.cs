using System.Collections.Generic;
using System.Data;

namespace CWI.BooMapper.Services.Relational
{
    /// <summary>
    /// Provides methods for materializing DataReaders into objects.
    /// </summary>
    public interface IRelationalMapperService
    {
        /// <summary>
        /// Map a single object.
        /// </summary>
        /// <typeparam name="TResult">The type of the object.</typeparam>
        /// <param name="key">
        /// 
        /// A Unique Key for this Mapper. 
        /// Mapper functions are generated once per key.
        /// This key identifies wich mapper function will be used. 
        /// 
        /// 
        /// </param>
        /// <param name="reader">The DataReader.</param>
        /// <example>
        /// Both calls use the same mapping function
        /// myService.Map<MyObject>("MyObjectSingleMapping", reader);
        /// myService.Map<MyObject>("MyObjectSingleMapping", reader);
        /// 
        /// This call uses another mapping function
        /// myService.Map<MyObject>("MyObjectSingleMappingAggreates", reader);
        /// </example>
        /// <returns></returns>
        TResult Map<TResult>(string key, IDataReader reader);

        /// <summary>
        /// Map a collection object.
        /// </summary>
        /// <typeparam name="TResult">The type of the object.</typeparam>
        /// <param name="key">
        /// 
        /// A Unique Key for this Mapper. 
        /// Mapper functions are generated once per key.
        /// This key identifies wich mapper function will be used. 
        /// 
        /// 
        /// </param>
        /// <param name="reader">The DataReader.</param>
        /// <example>
        /// Both calls use the same mapping function
        /// myService.MapCollection<MyObject>("MyObjectList", reader);
        /// myService.MapCollection<MyObject>("MyObjectList", reader);
        /// 
        /// This call uses another mapping function
        /// myService.MapCollection<MyObject>("MyObjectPaginatedSearch", reader);
        /// </example>
        /// <returns></returns>
        IEnumerable<TResult> MapCollection<TResult>(string key, IDataReader reader);
    }
}
