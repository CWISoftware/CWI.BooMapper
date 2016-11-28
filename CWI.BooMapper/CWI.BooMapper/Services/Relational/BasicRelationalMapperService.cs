using System;
using System.Collections.Generic;
using System.Data;
using CWI.BooMapper.Core;
using CWI.BooMapper.Core.Relational;

namespace CWI.BooMapper.Services.Relational
{
    /// <summary>
    /// Provides basic implementation off <see cref="IRelationalMapperService"/>
    /// </summary>
    public class BasicRelationalMapperService : IRelationalMapperService
    {
        private RelationalMapperSettings settings;

        public BasicRelationalMapperService()
        {
            settings = new RelationalMapperSettings()
            {
                DisposeReader = true
            };
        }

        public RelationalMapperSettings Settings
        {
            get
            {
                return settings;
            }
            set
            {
                if (value != null)
                {
                    settings = value;
                }
            }
        }

        public TResult Map<TResult>(string key, IDataReader reader)
        {
            return OnMap<TResult>(GetOrGenerateMapper<TResult>(key, reader), reader);
        }

        public TResult Map<TResult>(RelationalMapper mapper, IDataReader reader)
        {
            ThrowHelper.ThrowArgumentNullException(mapper, nameof(mapper));
            ThrowHelper.ThrowArgumentNullException(reader, nameof(reader));

            return OnMap<TResult>(mapper, reader);
        }

        public IEnumerable<TResult> MapCollection<TResult>(string key, IDataReader reader)
        {
            return OnMapCollection<TResult>(GetOrGenerateMapper<TResult>(key, reader), reader);
        }

        public IEnumerable<TResult> MapCollection<TResult>(RelationalMapper mapper, IDataReader reader)
        {
            ThrowHelper.ThrowArgumentNullException(mapper, nameof(mapper));
            ThrowHelper.ThrowArgumentNullException(reader, nameof(reader));

            return OnMapCollection<TResult>(mapper, reader);
        }

        public RelationalMapper GetOrGenerateMapper<TResult>(string key, IDataReader reader)
        {
            RelationalMapper mapper;

            if (!RelationalMapperCache.TryGet(key, out mapper))
            {
                mapper = OnGenerateMapper(typeof(TResult), key, reader);
            }

            return mapper;
        }

        private RelationalMapper OnGenerateMapper(Type type, string key, IDataReader reader)
        {
            RelationalMapperCache.Add(key, new RelationalMapBuilder(type).Generate(reader, key));
            return RelationalMapperCache.Get(key);
        }

        private TResult OnMap<TResult>(RelationalMapper mapper, IDataReader reader)
        {
            try
            {
                TResult result;

                if (reader.Read())
                {
                    result = (TResult)mapper(reader);
                }
                else
                {
                    result = default(TResult);
                }

                return result;
            }
            finally
            {
                if (settings.DisposeReader)
                {
                    reader.Dispose();
                }
            }
        }

        private IEnumerable<TResult> OnMapCollection<TResult>(RelationalMapper mapper, IDataReader reader)
        {
            try
            {
                List<TResult> list = new List<TResult>();

                while (reader.Read())
                {
                    list.Add((TResult)mapper(reader));
                }

                return list;
            }
            finally
            {
                if (settings.DisposeReader)
                {
                    reader.Dispose();
                }
            }
        }
    }
}
