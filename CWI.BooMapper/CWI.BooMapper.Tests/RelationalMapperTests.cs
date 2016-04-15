using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using CWI.BooMapper.Core.Relational;
using CWI.BooMapper.Services.Relational;
using CWI.BooMapper.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CWI.BooMapper.Tests
{
    [TestClass]
    public class RelationalMapperTests
    {
        private BasicRelationalMapperService service;

        [TestInitialize]
        public void Initialize()
        {
            RelationalMapperCache.Clear();

            service = new BasicRelationalMapperService()
            {
                Settings = new RelationalMapperSettings()
                {
                    DisposeReader = false
                }
            };
        }

        private const string MapName = "MapClass";

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapBoolean()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Boolean));
            reader.AddValues(true);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Boolean, true);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        [ExpectedException(typeof(Core.Relational.DataException))]
        public void TestInvalidCastExceptionHandler()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Boolean));
            reader.AddValues("not a bool");

            service.Map<MapClass1>(MapName, reader);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapBooleanNullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.BooleanNullable));
            reader.AddValues((bool?)true);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.BooleanNullable, true);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapInt32()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Int32));
            reader.AddValues((100));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Int32, 100);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapUInt32()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.UInt32));
            reader.AddValues(((uint)1000));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.UInt32, (uint)1000);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapUInt32Nullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.UInt32Nullable));
            reader.AddValues(((uint?)1000));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.UInt32Nullable, (uint?)1000);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapInt32Nullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Int32Nullable));
            reader.AddValues(((int?)100));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Int32Nullable, 100);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapInt16()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Int16));
            reader.AddValues(((short)50));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Int16, (short)50);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapInt16Nullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Int16Nullable));
            reader.AddValues(((short?)50));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Int16Nullable, (short?)50);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapUInt16()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.UInt16));
            reader.AddValues(((ushort)12));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.UInt16, (ushort)12);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapUInt16Nullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.UInt16Nullable));
            reader.AddValues(((ushort?)11));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.UInt16Nullable, (ushort?)11);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapInt64()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Int64));
            reader.AddValues(((long)777777));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Int64, 777777);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapInt64Nullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Int64Nullable));
            reader.AddValues(((long?)777777));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Int64Nullable, 777777);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapUInt64()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.UInt64));
            reader.AddValues(((ulong)5555));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.UInt64, (ulong)5555);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapUInt64Nullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.UInt64Nullable));
            reader.AddValues(((ulong?)55554));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.UInt64Nullable, (ulong?)55554);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapFloat()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Float));
            reader.AddValues(((float)1.1111));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Float, (float)1.1111);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapFloatNullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.FloatNullable));
            reader.AddValues(((float?)1.1111));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.FloatNullable, (float?)1.1111);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapDouble()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Double));
            reader.AddValues((1.1111111));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Double, 1.1111111);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapDoubleNullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.DoubleNullable));
            reader.AddValues(((double?)2.1111));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.DoubleNullable, (double?)2.1111);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapDecimal()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Decimal));
            reader.AddValues((decimal.MaxValue));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Decimal, decimal.MaxValue);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapDecimalNullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.DecimalNullable));
            reader.AddValues((decimal?)decimal.MaxValue - 1);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.DecimalNullable, (decimal?)decimal.MaxValue - 1);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapByte()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Byte));
            reader.AddValues(byte.MinValue);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Byte, byte.MinValue);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapByteNullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.ByteNullable));
            reader.AddValues((byte?)5);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ByteNullable, (byte?)5);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapSByte()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.SByte));
            reader.AddValues(sbyte.MinValue);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.SByte, sbyte.MinValue);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapSByteNullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.SByteNullable));
            reader.AddValues((sbyte?)1);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.SByteNullable, (sbyte?)1);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapDateTime()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.DateTime));
            reader.AddValues(new DateTime(2000, 01, 01));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.DateTime, new DateTime(2000, 01, 01));
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapDateTimeNullable()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.DateTimeNullable));
            reader.AddValues((DateTime?)new DateTime(2001, 01, 01));

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.DateTimeNullable, (DateTime?)new DateTime(2001, 01, 01));
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapGuid()
        {
            Guid g = Guid.NewGuid();

            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Guid));
            reader.AddValues(g);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Guid, g);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapGuidNullable()
        {
            Guid? g = Guid.NewGuid();

            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.GuidNullable));
            reader.AddValues(g);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GuidNullable, g);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapGuidAsString()
        {
            string g = Guid.NewGuid().ToString();

            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Guid));
            reader.AddValues(g);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Guid, new Guid(g));
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapGuidNullableAsString()
        {
            string g = Guid.NewGuid().ToString();

            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.GuidNullable));
            reader.AddValues(g);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GuidNullable, new Guid(g));
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapObjectAsInt()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Object));
            reader.AddValues(900);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Object, 900);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapObjectAsString()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Object));
            reader.AddValues("teste");

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Object, "teste");
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapBaseClassObjectAsInt()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClassBase.BaseObj));
            reader.AddValues(500);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.BaseObj, 500);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapString()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.String));
            reader.AddValues("Hello World");

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.String, "Hello World");
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapFullObject()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Boolean),
                              nameof(MapClass1.BooleanNullable),
                              nameof(MapClass1.Byte),
                              nameof(MapClass1.ByteNullable),
                              nameof(MapClass1.DateTime),
                              nameof(MapClass1.DateTimeNullable),
                              nameof(MapClass1.Decimal),
                              nameof(MapClass1.DecimalNullable),
                              nameof(MapClass1.Double),
                              nameof(MapClass1.DoubleNullable),
                              nameof(MapClass1.Float),
                              nameof(MapClass1.FloatNullable),
                              nameof(MapClass1.Guid),
                              nameof(MapClass1.GuidNullable),
                              nameof(MapClass1.Int16),
                              nameof(MapClass1.Int16Nullable),
                              nameof(MapClass1.Int32),
                              nameof(MapClass1.Int32Nullable),
                              nameof(MapClass1.Int64),
                              nameof(MapClass1.Int64Nullable),
                              nameof(MapClass1.Object),
                              nameof(MapClass1.SByte),
                              nameof(MapClass1.SByteNullable),
                              nameof(MapClass1.String),
                              nameof(MapClass1.UInt16),
                              nameof(MapClass1.UInt16Nullable),
                              nameof(MapClass1.UInt32),
                              nameof(MapClass1.UInt32Nullable),
                              nameof(MapClass1.UInt64),
                              nameof(MapClass1.UInt64Nullable));
            reader.AddValues(false,
                             (bool?)true,
                             (byte)10,
                             (byte?)20,
                             new DateTime(2001, 01, 02, 05, 30, 30),
                             (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                             (decimal)999999,
                             (decimal?)55555,
                             (double)111111,
                             (double?)22222,
                             (float)10.1000,
                             (float?)11.1111,
                             new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                             (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                             (short)90,
                             (short?)91,
                             32767,
                             (int?)32768,
                             (long)234234234234,
                             (long?)234234234,
                             (int)int.MinValue,
                             (sbyte)3,
                             (sbyte?)4,
                             "Teste String",
                             (ushort)99,
                             (ushort?)100,
                             (uint)67676,
                             (uint?)87878,
                             (ulong)7767567567567,
                             (ulong?)456456456456456);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.AreEqual(result.Boolean, false);
            Assert.AreEqual(result.BooleanNullable, true);
            Assert.AreEqual(result.Byte, 10);
            Assert.AreEqual(result.ByteNullable, (byte?)20);
            Assert.AreEqual(result.DateTime, new DateTime(2001, 01, 02, 05, 30, 30));
            Assert.AreEqual(result.DateTimeNullable, (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31));
            Assert.AreEqual(result.Decimal, 999999);
            Assert.AreEqual(result.DecimalNullable, 55555);
            Assert.AreEqual(result.Double, 111111);
            Assert.AreEqual(result.DoubleNullable, 22222);
            Assert.AreEqual(result.Float, (float)10.1000);
            Assert.AreEqual(result.FloatNullable, (float?)11.1111);
            Assert.AreEqual(result.Guid, new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"));
            Assert.AreEqual(result.GuidNullable, new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"));
            Assert.AreEqual(result.Int16, 90);
            Assert.AreEqual(result.Int16Nullable, (short?)91);
            Assert.AreEqual(result.Int32, 32767);
            Assert.AreEqual(result.Int32Nullable, 32768);
            Assert.AreEqual(result.Int64, 234234234234);
            Assert.AreEqual(result.Int64Nullable, 234234234);
            Assert.AreEqual(result.Object, (int)int.MinValue);
            Assert.AreEqual(result.SByte, 3);
            Assert.AreEqual(result.SByteNullable, (sbyte?)4);
            Assert.AreEqual(result.String, "Teste String");
            Assert.AreEqual(result.UInt16, 99);
            Assert.AreEqual(result.UInt16Nullable, (ushort?)100);
            Assert.AreEqual(result.UInt32, (uint)67676);
            Assert.AreEqual(result.UInt32Nullable, (uint?)87878);
            Assert.AreEqual(result.UInt64, (ulong)7767567567567);
            Assert.AreEqual(result.UInt64Nullable, (ulong?)456456456456456);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapFullObjectWithNulls()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Boolean),
                              nameof(MapClass1.BooleanNullable),
                              nameof(MapClass1.Byte),
                              nameof(MapClass1.ByteNullable),
                              nameof(MapClass1.DateTime),
                              nameof(MapClass1.DateTimeNullable),
                              nameof(MapClass1.Decimal),
                              nameof(MapClass1.DecimalNullable),
                              nameof(MapClass1.Double),
                              nameof(MapClass1.DoubleNullable),
                              nameof(MapClass1.Float),
                              nameof(MapClass1.FloatNullable),
                              nameof(MapClass1.Guid),
                              nameof(MapClass1.GuidNullable),
                              nameof(MapClass1.Int16),
                              nameof(MapClass1.Int16Nullable),
                              nameof(MapClass1.Int32),
                              nameof(MapClass1.Int32Nullable),
                              nameof(MapClass1.Int64),
                              nameof(MapClass1.Int64Nullable),
                              nameof(MapClass1.Object),
                              nameof(MapClass1.SByte),
                              nameof(MapClass1.SByteNullable),
                              nameof(MapClass1.String),
                              nameof(MapClass1.UInt16),
                              nameof(MapClass1.UInt16Nullable),
                              nameof(MapClass1.UInt32),
                              nameof(MapClass1.UInt32Nullable),
                              nameof(MapClass1.UInt64),
                              nameof(MapClass1.UInt64Nullable));
            reader.AddValues(false,
                             null,
                             (byte)10,
                             null,
                             new DateTime(2001, 01, 02, 05, 30, 30),
                             null,
                             (decimal)999999,
                             null,
                             (double)111111,
                             null,
                             (float)10.1000,
                             null,
                             new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                             null,
                             (short)90,
                             null,
                             32767,
                             null,
                             (long)234234234234,
                             null,
                             null,
                             (sbyte)3,
                             null,
                             "Teste String",
                             (ushort)99,
                             null,
                             (uint)67676,
                             null,
                             (ulong)7767567567567,
                             null);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);
            Assert.AreEqual(result.Boolean, false);
            Assert.AreEqual(result.BooleanNullable, null);
            Assert.AreEqual(result.Byte, 10);
            Assert.AreEqual(result.ByteNullable, null);
            Assert.AreEqual(result.DateTime, new DateTime(2001, 01, 02, 05, 30, 30));
            Assert.AreEqual(result.DateTimeNullable, null);
            Assert.AreEqual(result.Decimal, 999999);
            Assert.AreEqual(result.DecimalNullable, null);
            Assert.AreEqual(result.Double, 111111);
            Assert.AreEqual(result.DoubleNullable, null);
            Assert.AreEqual(result.Float, (float)10.1000);
            Assert.AreEqual(result.FloatNullable, null);
            Assert.AreEqual(result.Guid, new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"));
            Assert.AreEqual(result.GuidNullable, null);
            Assert.AreEqual(result.Int16, 90);
            Assert.AreEqual(result.Int16Nullable, null);
            Assert.AreEqual(result.Int32, 32767);
            Assert.AreEqual(result.Int32Nullable, null);
            Assert.AreEqual(result.Int64, 234234234234);
            Assert.AreEqual(result.Int64Nullable, null);
            Assert.AreEqual(result.Object, null);
            Assert.AreEqual(result.SByte, 3);
            Assert.AreEqual(result.SByteNullable, null);
            Assert.AreEqual(result.String, "Teste String");
            Assert.AreEqual(result.UInt16, 99);
            Assert.AreEqual(result.UInt16Nullable, null);
            Assert.AreEqual(result.UInt32, (uint)67676);
            Assert.AreEqual(result.UInt32Nullable, null);
            Assert.AreEqual(result.UInt64, (ulong)7767567567567);
            Assert.AreEqual(result.UInt64Nullable, null);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapFullNestedObject()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Boolean),
                              nameof(MapClass1.BooleanNullable),
                              nameof(MapClass1.Byte),
                              nameof(MapClass1.ByteNullable),
                              nameof(MapClass1.DateTime),
                              nameof(MapClass1.DateTimeNullable),
                              nameof(MapClass1.Decimal),
                              nameof(MapClass1.DecimalNullable),
                              nameof(MapClass1.Double),
                              nameof(MapClass1.DoubleNullable),
                              nameof(MapClass1.Float),
                              nameof(MapClass1.FloatNullable),
                              nameof(MapClass1.Guid),
                              nameof(MapClass1.GuidNullable),
                              nameof(MapClass1.Int16),
                              nameof(MapClass1.Int16Nullable),
                              nameof(MapClass1.Int32),
                              nameof(MapClass1.Int32Nullable),
                              nameof(MapClass1.Int64),
                              nameof(MapClass1.Int64Nullable),
                              nameof(MapClass1.Object),
                              nameof(MapClass1.SByte),
                              nameof(MapClass1.SByteNullable),
                              nameof(MapClass1.String),
                              nameof(MapClass1.UInt16),
                              nameof(MapClass1.UInt16Nullable),
                              nameof(MapClass1.UInt32),
                              nameof(MapClass1.UInt32Nullable),
                              nameof(MapClass1.UInt64),
                              nameof(MapClass1.UInt64Nullable));

            reader.AddColumns(nameof(MapClass1.Nested) + "." + nameof(MapClass1.Boolean),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.BooleanNullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.Byte),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.ByteNullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.DateTime),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.DateTimeNullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.Decimal),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.DecimalNullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.Double),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.DoubleNullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.Float),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.FloatNullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.Guid),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.GuidNullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.Int16),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.Int16Nullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.Int32),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.Int32Nullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.Int64),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.Int64Nullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.Object),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.SByte),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.SByteNullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.String),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.UInt16),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.UInt16Nullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.UInt32),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.UInt32Nullable),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.UInt64),
                              nameof(MapClass1.Nested) + "." + nameof(MapClass1.UInt64Nullable));

            reader.AddValues(false,
                             (bool?)true,
                             (byte)10,
                             (byte?)20,
                             new DateTime(2001, 01, 02, 05, 30, 30),
                             (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                             (decimal)999999,
                             (decimal?)55555,
                             (double)111111,
                             (double?)22222,
                             (float)10.1000,
                             (float?)11.1111,
                             new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                             (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                             (short)90,
                             (short?)91,
                             32767,
                             (int?)32768,
                             (long)234234234234,
                             (long?)234234234,
                             (int)int.MinValue,
                             (sbyte)3,
                             (sbyte?)4,
                             "Teste String",
                             (ushort)99,
                             (ushort?)100,
                             (uint)67676,
                             (uint?)87878,
                             (ulong)7767567567567,
                             (ulong?)456456456456456,
                             false,
                             (bool?)true,
                             (byte)10,
                             (byte?)20,
                             new DateTime(2001, 01, 02, 05, 30, 30),
                             (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                             (decimal)999999,
                             (decimal?)55555,
                             (double)111111,
                             (double?)22222,
                             (float)10.1000,
                             (float?)11.1111,
                             new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                             (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                             (short)90,
                             (short?)91,
                             32767,
                             (int?)32768,
                             (long)234234234234,
                             (long?)234234234,
                             (int)int.MinValue,
                             (sbyte)3,
                             (sbyte?)4,
                             "Teste String",
                             (ushort)99,
                             (ushort?)100,
                             (uint)67676,
                             (uint?)87878,
                             (ulong)7767567567567,
                             (ulong?)456456456456456);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);

            Assert.AreEqual(result.Boolean, false);
            Assert.AreEqual(result.BooleanNullable, true);
            Assert.AreEqual(result.Byte, 10);
            Assert.AreEqual(result.ByteNullable, (byte?)20);
            Assert.AreEqual(result.DateTime, new DateTime(2001, 01, 02, 05, 30, 30));
            Assert.AreEqual(result.DateTimeNullable, (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31));
            Assert.AreEqual(result.Decimal, 999999);
            Assert.AreEqual(result.DecimalNullable, 55555);
            Assert.AreEqual(result.Double, 111111);
            Assert.AreEqual(result.DoubleNullable, 22222);
            Assert.AreEqual(result.Float, (float)10.1000);
            Assert.AreEqual(result.FloatNullable, (float?)11.1111);
            Assert.AreEqual(result.Guid, new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"));
            Assert.AreEqual(result.GuidNullable, new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"));
            Assert.AreEqual(result.Int16, 90);
            Assert.AreEqual(result.Int16Nullable, (short?)91);
            Assert.AreEqual(result.Int32, 32767);
            Assert.AreEqual(result.Int32Nullable, 32768);
            Assert.AreEqual(result.Int64, 234234234234);
            Assert.AreEqual(result.Int64Nullable, 234234234);
            Assert.AreEqual(result.Object, (int)int.MinValue);
            Assert.AreEqual(result.SByte, 3);
            Assert.AreEqual(result.SByteNullable, (sbyte?)4);
            Assert.AreEqual(result.String, "Teste String");
            Assert.AreEqual(result.UInt16, 99);
            Assert.AreEqual(result.UInt16Nullable, (ushort?)100);
            Assert.AreEqual(result.UInt32, (uint)67676);
            Assert.AreEqual(result.UInt32Nullable, (uint?)87878);
            Assert.AreEqual(result.UInt64, (ulong)7767567567567);
            Assert.AreEqual(result.UInt64Nullable, (ulong?)456456456456456);

            Assert.AreEqual(result.Nested.Boolean, false);
            Assert.AreEqual(result.Nested.BooleanNullable, true);
            Assert.AreEqual(result.Nested.Byte, 10);
            Assert.AreEqual(result.Nested.ByteNullable, (byte?)20);
            Assert.AreEqual(result.Nested.DateTime, new DateTime(2001, 01, 02, 05, 30, 30));
            Assert.AreEqual(result.Nested.DateTimeNullable, (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31));
            Assert.AreEqual(result.Nested.Decimal, 999999);
            Assert.AreEqual(result.Nested.DecimalNullable, 55555);
            Assert.AreEqual(result.Nested.Double, 111111);
            Assert.AreEqual(result.Nested.DoubleNullable, 22222);
            Assert.AreEqual(result.Nested.Float, (float)10.1000);
            Assert.AreEqual(result.Nested.FloatNullable, (float?)11.1111);
            Assert.AreEqual(result.Nested.Guid, new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"));
            Assert.AreEqual(result.Nested.GuidNullable, new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"));
            Assert.AreEqual(result.Nested.Int16, 90);
            Assert.AreEqual(result.Nested.Int16Nullable, (short?)91);
            Assert.AreEqual(result.Nested.Int32, 32767);
            Assert.AreEqual(result.Nested.Int32Nullable, 32768);
            Assert.AreEqual(result.Nested.Int64, 234234234234);
            Assert.AreEqual(result.Nested.Int64Nullable, 234234234);
            Assert.AreEqual(result.Nested.Object, (int)int.MinValue);
            Assert.AreEqual(result.Nested.SByte, 3);
            Assert.AreEqual(result.Nested.SByteNullable, (sbyte?)4);
            Assert.AreEqual(result.Nested.String, "Teste String");
            Assert.AreEqual(result.Nested.UInt16, 99);
            Assert.AreEqual(result.Nested.UInt16Nullable, (ushort?)100);
            Assert.AreEqual(result.Nested.UInt32, (uint)67676);
            Assert.AreEqual(result.Nested.UInt32Nullable, (uint?)87878);
            Assert.AreEqual(result.Nested.UInt64, (ulong)7767567567567);
            Assert.AreEqual(result.Nested.UInt64Nullable, (ulong?)456456456456456);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapFullMultipleNestedbjects()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Boolean),
                              nameof(MapClass1.BooleanNullable),
                              nameof(MapClass1.Byte),
                              nameof(MapClass1.ByteNullable),
                              nameof(MapClass1.DateTime),
                              nameof(MapClass1.DateTimeNullable),
                              nameof(MapClass1.Decimal),
                              nameof(MapClass1.DecimalNullable),
                              nameof(MapClass1.Double),
                              nameof(MapClass1.DoubleNullable),
                              nameof(MapClass1.Float),
                              nameof(MapClass1.FloatNullable),
                              nameof(MapClass1.Guid),
                              nameof(MapClass1.GuidNullable),
                              nameof(MapClass1.Int16),
                              nameof(MapClass1.Int16Nullable),
                              nameof(MapClass1.Int32),
                              nameof(MapClass1.Int32Nullable),
                              nameof(MapClass1.Int64),
                              nameof(MapClass1.Int64Nullable),
                              nameof(MapClass1.Object),
                              nameof(MapClass1.SByte),
                              nameof(MapClass1.SByteNullable),
                              nameof(MapClass1.String),
                              nameof(MapClass1.UInt16),
                              nameof(MapClass1.UInt16Nullable),
                              nameof(MapClass1.UInt32),
                              nameof(MapClass1.UInt32Nullable),
                              nameof(MapClass1.UInt64),
                              nameof(MapClass1.UInt64Nullable),

                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Boolean),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.BooleanNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Byte),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.ByteNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.DateTime),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.DateTimeNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Decimal),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.DecimalNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Double),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.DoubleNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Float),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.FloatNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Guid),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.GuidNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Int16),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Int16Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Int32),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Int32Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Int64),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Int64Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.Object),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.SByte),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.SByteNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.String),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.UInt16),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.UInt16Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.UInt32),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.UInt32Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.UInt64),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass1.UInt64Nullable),

                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Boolean),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.BooleanNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Byte),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.ByteNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.DateTime),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.DateTimeNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Decimal),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.DecimalNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Double),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.DoubleNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Float),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.FloatNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Guid),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.GuidNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Int16),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Int16Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Int32),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Int32Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Int64),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Int64Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.Object),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.SByte),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.SByteNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.String),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.UInt16),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.UInt16Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.UInt32),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.UInt32Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.UInt64),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass1.UInt64Nullable),

                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Boolean),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.BooleanNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Byte),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.ByteNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.DateTime),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.DateTimeNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Decimal),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.DecimalNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Double),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.DoubleNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Float),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.FloatNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Guid),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.GuidNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Int16),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Int16Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Int32),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Int32Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Int64),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Int64Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.Object),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.SByte),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.SByteNullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.String),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.UInt16),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.UInt16Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.UInt32),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.UInt32Nullable),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.UInt64),
                              nameof(MapClass1.Class2) + "." + nameof(MapClass2.Class3) + "." + nameof(MapClass3.Class4) + "." + nameof(MapClass1.UInt64Nullable));

            reader.AddValues(false,
                             (bool?)true,
                             (byte)10,
                             (byte?)20,
                             new DateTime(2001, 01, 02, 05, 30, 30),
                             (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                             (decimal)999999,
                             (decimal?)55555,
                             (double)111111,
                             (double?)22222,
                             (float)10.1000,
                             (float?)11.1111,
                             new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                             (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                             (short)90,
                             (short?)91,
                             32767,
                             (int?)32768,
                             (long)234234234234,
                             (long?)234234234,
                             (int)int.MinValue,
                             (sbyte)3,
                             (sbyte?)4,
                             "Teste String",
                             (ushort)99,
                             (ushort?)100,
                             (uint)67676,
                             (uint?)87878,
                             (ulong)7767567567567,
                             (ulong?)456456456456456,

                             false,
                             (bool?)true,
                             (byte)10,
                             (byte?)20,
                             new DateTime(2001, 01, 02, 05, 30, 30),
                             (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                             (decimal)999999,
                             (decimal?)55555,
                             (double)111111,
                             (double?)22222,
                             (float)10.1000,
                             (float?)11.1111,
                             new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                             (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                             (short)90,
                             (short?)91,
                             32767,
                             (int?)32768,
                             (long)234234234234,
                             (long?)234234234,
                             (int)int.MinValue,
                             (sbyte)3,
                             (sbyte?)4,
                             "Teste String class 2",
                             (ushort)99,
                             (ushort?)100,
                             (uint)67676,
                             (uint?)87878,
                             (ulong)7767567567567,
                             (ulong?)456456456456456,

                             false,
                             (bool?)true,
                             (byte)10,
                             (byte?)20,
                             new DateTime(2001, 01, 02, 05, 30, 30),
                             (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                             (decimal)999999,
                             (decimal?)55555,
                             (double)111111,
                             (double?)22222,
                             (float)10.1000,
                             (float?)11.1111,
                             new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                             (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                             (short)90,
                             (short?)91,
                             32767,
                             (int?)32768,
                             (long)234234234234,
                             (long?)234234234,
                             (int)int.MinValue,
                             (sbyte)3,
                             (sbyte?)4,
                             "Teste String class 3",
                             (ushort)99,
                             (ushort?)100,
                             (uint)67676,
                             (uint?)87878,
                             (ulong)7767567567567,
                             (ulong?)456456456456456,

                             false,
                             (bool?)true,
                             (byte)10,
                             (byte?)20,
                             new DateTime(2001, 01, 02, 05, 30, 30),
                             (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                             (decimal)999999,
                             (decimal?)55555,
                             (double)111111,
                             (double?)22222,
                             (float)10.1000,
                             (float?)11.1111,
                             new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                             (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                             (short)90,
                             (short?)91,
                             32767,
                             (int?)32768,
                             (long)234234234234,
                             (long?)234234234,
                             (int)int.MinValue,
                             (sbyte)3,
                             (sbyte?)4,
                             "Teste String class 4",
                             (ushort)99,
                             (ushort?)100,
                             (uint)67676,
                             (uint?)87878,
                             (ulong)7767567567567,
                             (ulong?)456456456456456);

            MapClass1 result = service.Map<MapClass1>(MapName, reader);

            Assert.AreEqual(result.Boolean, false);
            Assert.AreEqual(result.BooleanNullable, true);
            Assert.AreEqual(result.Byte, 10);
            Assert.AreEqual(result.ByteNullable, (byte?)20);
            Assert.AreEqual(result.DateTime, new DateTime(2001, 01, 02, 05, 30, 30));
            Assert.AreEqual(result.DateTimeNullable, (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31));
            Assert.AreEqual(result.Decimal, 999999);
            Assert.AreEqual(result.DecimalNullable, 55555);
            Assert.AreEqual(result.Double, 111111);
            Assert.AreEqual(result.DoubleNullable, 22222);
            Assert.AreEqual(result.Float, (float)10.1000);
            Assert.AreEqual(result.FloatNullable, (float?)11.1111);
            Assert.AreEqual(result.Guid, new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"));
            Assert.AreEqual(result.GuidNullable, new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"));
            Assert.AreEqual(result.Int16, 90);
            Assert.AreEqual(result.Int16Nullable, (short?)91);
            Assert.AreEqual(result.Int32, 32767);
            Assert.AreEqual(result.Int32Nullable, 32768);
            Assert.AreEqual(result.Int64, 234234234234);
            Assert.AreEqual(result.Int64Nullable, 234234234);
            Assert.AreEqual(result.Object, (int)int.MinValue);
            Assert.AreEqual(result.SByte, 3);
            Assert.AreEqual(result.SByteNullable, (sbyte?)4);
            Assert.AreEqual(result.String, "Teste String");
            Assert.AreEqual(result.UInt16, 99);
            Assert.AreEqual(result.UInt16Nullable, (ushort?)100);
            Assert.AreEqual(result.UInt32, (uint)67676);
            Assert.AreEqual(result.UInt32Nullable, (uint?)87878);
            Assert.AreEqual(result.UInt64, (ulong)7767567567567);
            Assert.AreEqual(result.UInt64Nullable, (ulong?)456456456456456);

            Assert.AreEqual(result.Class2.Boolean, false);
            Assert.AreEqual(result.Class2.BooleanNullable, true);
            Assert.AreEqual(result.Class2.Byte, 10);
            Assert.AreEqual(result.Class2.ByteNullable, (byte?)20);
            Assert.AreEqual(result.Class2.DateTime, new DateTime(2001, 01, 02, 05, 30, 30));
            Assert.AreEqual(result.Class2.DateTimeNullable, (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31));
            Assert.AreEqual(result.Class2.Decimal, 999999);
            Assert.AreEqual(result.Class2.DecimalNullable, 55555);
            Assert.AreEqual(result.Class2.Double, 111111);
            Assert.AreEqual(result.Class2.DoubleNullable, 22222);
            Assert.AreEqual(result.Class2.Float, (float)10.1000);
            Assert.AreEqual(result.Class2.FloatNullable, (float?)11.1111);
            Assert.AreEqual(result.Class2.Guid, new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"));
            Assert.AreEqual(result.Class2.GuidNullable, new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"));
            Assert.AreEqual(result.Class2.Int16, 90);
            Assert.AreEqual(result.Class2.Int16Nullable, (short?)91);
            Assert.AreEqual(result.Class2.Int32, 32767);
            Assert.AreEqual(result.Class2.Int32Nullable, 32768);
            Assert.AreEqual(result.Class2.Int64, 234234234234);
            Assert.AreEqual(result.Class2.Int64Nullable, 234234234);
            Assert.AreEqual(result.Class2.Object, (int)int.MinValue);
            Assert.AreEqual(result.Class2.SByte, 3);
            Assert.AreEqual(result.Class2.SByteNullable, (sbyte?)4);
            Assert.AreEqual(result.Class2.String, "Teste String class 2");
            Assert.AreEqual(result.Class2.UInt16, 99);
            Assert.AreEqual(result.Class2.UInt16Nullable, (ushort?)100);
            Assert.AreEqual(result.Class2.UInt32, (uint)67676);
            Assert.AreEqual(result.Class2.UInt32Nullable, (uint?)87878);
            Assert.AreEqual(result.Class2.UInt64, (ulong)7767567567567);
            Assert.AreEqual(result.Class2.UInt64Nullable, (ulong?)456456456456456);

            Assert.AreEqual(result.Class2.Class3.Boolean, false);
            Assert.AreEqual(result.Class2.Class3.BooleanNullable, true);
            Assert.AreEqual(result.Class2.Class3.Byte, 10);
            Assert.AreEqual(result.Class2.Class3.ByteNullable, (byte?)20);
            Assert.AreEqual(result.Class2.Class3.DateTime, new DateTime(2001, 01, 02, 05, 30, 30));
            Assert.AreEqual(result.Class2.Class3.DateTimeNullable, (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31));
            Assert.AreEqual(result.Class2.Class3.Decimal, 999999);
            Assert.AreEqual(result.Class2.Class3.DecimalNullable, 55555);
            Assert.AreEqual(result.Class2.Class3.Double, 111111);
            Assert.AreEqual(result.Class2.Class3.DoubleNullable, 22222);
            Assert.AreEqual(result.Class2.Class3.Float, (float)10.1000);
            Assert.AreEqual(result.Class2.Class3.FloatNullable, (float?)11.1111);
            Assert.AreEqual(result.Class2.Class3.Guid, new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"));
            Assert.AreEqual(result.Class2.Class3.GuidNullable, new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"));
            Assert.AreEqual(result.Class2.Class3.Int16, 90);
            Assert.AreEqual(result.Class2.Class3.Int16Nullable, (short?)91);
            Assert.AreEqual(result.Class2.Class3.Int32, 32767);
            Assert.AreEqual(result.Class2.Class3.Int32Nullable, 32768);
            Assert.AreEqual(result.Class2.Class3.Int64, 234234234234);
            Assert.AreEqual(result.Class2.Class3.Int64Nullable, 234234234);
            Assert.AreEqual(result.Class2.Class3.Object, (int)int.MinValue);
            Assert.AreEqual(result.Class2.Class3.SByte, 3);
            Assert.AreEqual(result.Class2.Class3.SByteNullable, (sbyte?)4);
            Assert.AreEqual(result.Class2.Class3.String, "Teste String class 3");
            Assert.AreEqual(result.Class2.Class3.UInt16, 99);
            Assert.AreEqual(result.Class2.Class3.UInt16Nullable, (ushort?)100);
            Assert.AreEqual(result.Class2.Class3.UInt32, (uint)67676);
            Assert.AreEqual(result.Class2.Class3.UInt32Nullable, (uint?)87878);
            Assert.AreEqual(result.Class2.Class3.UInt64, (ulong)7767567567567);
            Assert.AreEqual(result.Class2.Class3.UInt64Nullable, (ulong?)456456456456456);

            Assert.AreEqual(result.Class2.Class3.Class4.Boolean, false);
            Assert.AreEqual(result.Class2.Class3.Class4.BooleanNullable, true);
            Assert.AreEqual(result.Class2.Class3.Class4.Byte, 10);
            Assert.AreEqual(result.Class2.Class3.Class4.ByteNullable, (byte?)20);
            Assert.AreEqual(result.Class2.Class3.Class4.DateTime, new DateTime(2001, 01, 02, 05, 30, 30));
            Assert.AreEqual(result.Class2.Class3.Class4.DateTimeNullable, (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31));
            Assert.AreEqual(result.Class2.Class3.Class4.Decimal, 999999);
            Assert.AreEqual(result.Class2.Class3.Class4.DecimalNullable, 55555);
            Assert.AreEqual(result.Class2.Class3.Class4.Double, 111111);
            Assert.AreEqual(result.Class2.Class3.Class4.DoubleNullable, 22222);
            Assert.AreEqual(result.Class2.Class3.Class4.Float, (float)10.1000);
            Assert.AreEqual(result.Class2.Class3.Class4.FloatNullable, (float?)11.1111);
            Assert.AreEqual(result.Class2.Class3.Class4.Guid, new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"));
            Assert.AreEqual(result.Class2.Class3.Class4.GuidNullable, new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"));
            Assert.AreEqual(result.Class2.Class3.Class4.Int16, 90);
            Assert.AreEqual(result.Class2.Class3.Class4.Int16Nullable, (short?)91);
            Assert.AreEqual(result.Class2.Class3.Class4.Int32, 32767);
            Assert.AreEqual(result.Class2.Class3.Class4.Int32Nullable, 32768);
            Assert.AreEqual(result.Class2.Class3.Class4.Int64, 234234234234);
            Assert.AreEqual(result.Class2.Class3.Class4.Int64Nullable, 234234234);
            Assert.AreEqual(result.Class2.Class3.Class4.Object, (int)int.MinValue);
            Assert.AreEqual(result.Class2.Class3.Class4.SByte, 3);
            Assert.AreEqual(result.Class2.Class3.Class4.SByteNullable, (sbyte?)4);
            Assert.AreEqual(result.Class2.Class3.Class4.String, "Teste String class 4");
            Assert.AreEqual(result.Class2.Class3.Class4.UInt16, 99);
            Assert.AreEqual(result.Class2.Class3.Class4.UInt16Nullable, (ushort?)100);
            Assert.AreEqual(result.Class2.Class3.Class4.UInt32, (uint)67676);
            Assert.AreEqual(result.Class2.Class3.Class4.UInt32Nullable, (uint?)87878);
            Assert.AreEqual(result.Class2.Class3.Class4.UInt64, (ulong)7767567567567);
            Assert.AreEqual(result.Class2.Class3.Class4.UInt64Nullable, (ulong?)456456456456456);
        }

        [TestMethod]
        [TestCategory("Mapping")]
        public void MapFullObjectCollection()
        {
            MemoryDataReader reader = new MemoryDataReader();
            reader.AddColumns(nameof(MapClass1.Boolean),
                              nameof(MapClass1.BooleanNullable),
                              nameof(MapClass1.Byte),
                              nameof(MapClass1.ByteNullable),
                              nameof(MapClass1.DateTime),
                              nameof(MapClass1.DateTimeNullable),
                              nameof(MapClass1.Decimal),
                              nameof(MapClass1.DecimalNullable),
                              nameof(MapClass1.Double),
                              nameof(MapClass1.DoubleNullable),
                              nameof(MapClass1.Float),
                              nameof(MapClass1.FloatNullable),
                              nameof(MapClass1.Guid),
                              nameof(MapClass1.GuidNullable),
                              nameof(MapClass1.Int16),
                              nameof(MapClass1.Int16Nullable),
                              nameof(MapClass1.Int32),
                              nameof(MapClass1.Int32Nullable),
                              nameof(MapClass1.Int64),
                              nameof(MapClass1.Int64Nullable),
                              nameof(MapClass1.Object),
                              nameof(MapClass1.SByte),
                              nameof(MapClass1.SByteNullable),
                              nameof(MapClass1.String),
                              nameof(MapClass1.UInt16),
                              nameof(MapClass1.UInt16Nullable),
                              nameof(MapClass1.UInt32),
                              nameof(MapClass1.UInt32Nullable),
                              nameof(MapClass1.UInt64),
                              nameof(MapClass1.UInt64Nullable));
            reader.AddValues(false,
                             (bool?)true,
                             (byte)10,
                             (byte?)20,
                             new DateTime(2001, 01, 02, 05, 30, 30),
                             (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                             (decimal)999999,
                             (decimal?)55555,
                             (double)111111,
                             (double?)22222,
                             (float)10.1000,
                             (float?)11.1111,
                             new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                             (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                             (short)90,
                             (short?)91,
                             32767,
                             (int?)32768,
                             (long)234234234234,
                             (long?)234234234,
                             (int)int.MinValue,
                             (sbyte)3,
                             (sbyte?)4,
                             "Teste String",
                             (ushort)99,
                             (ushort?)100,
                             (uint)67676,
                             (uint?)87878,
                             (ulong)7767567567567,
                             (ulong?)456456456456456);

            reader.AddValues(false,
                             (bool?)true,
                             (byte)10,
                             (byte?)20,
                             new DateTime(2001, 01, 02, 05, 30, 30),
                             (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                             (decimal)999999,
                             (decimal?)55555,
                             (double)111111,
                             (double?)22222,
                             (float)10.1000,
                             (float?)11.1111,
                             new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                             (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                             (short)90,
                             (short?)91,
                             32767,
                             (int?)32768,
                             (long)234234234234,
                             (long?)234234234,
                             (int)int.MinValue,
                             (sbyte)3,
                             (sbyte?)4,
                             "Teste String row 2",
                             (ushort)99,
                             (ushort?)100,
                             (uint)67676,
                             (uint?)87878,
                             (ulong)7767567567567,
                             (ulong?)456456456456456);

            IEnumerable<MapClass1> collection = service.MapCollection<MapClass1>(MapName, reader);
            Assert.AreEqual(2, collection.Count());

            MapClass1 result = collection.First();

            Assert.AreEqual(result.Boolean, false);
            Assert.AreEqual(result.BooleanNullable, true);
            Assert.AreEqual(result.Byte, 10);
            Assert.AreEqual(result.ByteNullable, (byte?)20);
            Assert.AreEqual(result.DateTime, new DateTime(2001, 01, 02, 05, 30, 30));
            Assert.AreEqual(result.DateTimeNullable, (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31));
            Assert.AreEqual(result.Decimal, 999999);
            Assert.AreEqual(result.DecimalNullable, 55555);
            Assert.AreEqual(result.Double, 111111);
            Assert.AreEqual(result.DoubleNullable, 22222);
            Assert.AreEqual(result.Float, (float)10.1000);
            Assert.AreEqual(result.FloatNullable, (float?)11.1111);
            Assert.AreEqual(result.Guid, new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"));
            Assert.AreEqual(result.GuidNullable, new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"));
            Assert.AreEqual(result.Int16, 90);
            Assert.AreEqual(result.Int16Nullable, (short?)91);
            Assert.AreEqual(result.Int32, 32767);
            Assert.AreEqual(result.Int32Nullable, 32768);
            Assert.AreEqual(result.Int64, 234234234234);
            Assert.AreEqual(result.Int64Nullable, 234234234);
            Assert.AreEqual(result.Object, (int)int.MinValue);
            Assert.AreEqual(result.SByte, 3);
            Assert.AreEqual(result.SByteNullable, (sbyte?)4);
            Assert.AreEqual(result.String, "Teste String");
            Assert.AreEqual(result.UInt16, 99);
            Assert.AreEqual(result.UInt16Nullable, (ushort?)100);
            Assert.AreEqual(result.UInt32, (uint)67676);
            Assert.AreEqual(result.UInt32Nullable, (uint?)87878);
            Assert.AreEqual(result.UInt64, (ulong)7767567567567);
            Assert.AreEqual(result.UInt64Nullable, (ulong?)456456456456456);

            result = collection.ElementAt(1);

            Assert.AreEqual(result.Boolean, false);
            Assert.AreEqual(result.BooleanNullable, true);
            Assert.AreEqual(result.Byte, 10);
            Assert.AreEqual(result.ByteNullable, (byte?)20);
            Assert.AreEqual(result.DateTime, new DateTime(2001, 01, 02, 05, 30, 30));
            Assert.AreEqual(result.DateTimeNullable, (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31));
            Assert.AreEqual(result.Decimal, 999999);
            Assert.AreEqual(result.DecimalNullable, 55555);
            Assert.AreEqual(result.Double, 111111);
            Assert.AreEqual(result.DoubleNullable, 22222);
            Assert.AreEqual(result.Float, (float)10.1000);
            Assert.AreEqual(result.FloatNullable, (float?)11.1111);
            Assert.AreEqual(result.Guid, new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"));
            Assert.AreEqual(result.GuidNullable, new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"));
            Assert.AreEqual(result.Int16, 90);
            Assert.AreEqual(result.Int16Nullable, (short?)91);
            Assert.AreEqual(result.Int32, 32767);
            Assert.AreEqual(result.Int32Nullable, 32768);
            Assert.AreEqual(result.Int64, 234234234234);
            Assert.AreEqual(result.Int64Nullable, 234234234);
            Assert.AreEqual(result.Object, (int)int.MinValue);
            Assert.AreEqual(result.SByte, 3);
            Assert.AreEqual(result.SByteNullable, (sbyte?)4);
            Assert.AreEqual(result.String, "Teste String row 2");
            Assert.AreEqual(result.UInt16, 99);
            Assert.AreEqual(result.UInt16Nullable, (ushort?)100);
            Assert.AreEqual(result.UInt32, (uint)67676);
            Assert.AreEqual(result.UInt32Nullable, (uint?)87878);
            Assert.AreEqual(result.UInt64, (ulong)7767567567567);
            Assert.AreEqual(result.UInt64Nullable, (ulong?)456456456456456);
        }

        [TestMethod]
        [TestCategory("Performance")]
        public void FasterThenHandCodeMappingForLargeCollections()
        {
            MemoryDataReader gmapperReader = new MemoryDataReader();
            MemoryDataReader handReader = new MemoryDataReader();

            gmapperReader.AddColumns(nameof(MapClass1.Boolean),
                              nameof(MapClass1.BooleanNullable),
                              nameof(MapClass1.Byte),
                              nameof(MapClass1.ByteNullable),
                              nameof(MapClass1.DateTime),
                              nameof(MapClass1.DateTimeNullable),
                              nameof(MapClass1.Decimal),
                              nameof(MapClass1.DecimalNullable),
                              nameof(MapClass1.Double),
                              nameof(MapClass1.DoubleNullable),
                              nameof(MapClass1.Float),
                              nameof(MapClass1.FloatNullable),
                              nameof(MapClass1.Guid),
                              nameof(MapClass1.GuidNullable),
                              nameof(MapClass1.Int16),
                              nameof(MapClass1.Int16Nullable),
                              nameof(MapClass1.Int32),
                              nameof(MapClass1.Int32Nullable),
                              nameof(MapClass1.Int64),
                              nameof(MapClass1.Int64Nullable),
                              nameof(MapClass1.Object),
                              nameof(MapClass1.SByte),
                              nameof(MapClass1.SByteNullable),
                              nameof(MapClass1.String),
                              nameof(MapClass1.UInt16),
                              nameof(MapClass1.UInt16Nullable),
                              nameof(MapClass1.UInt32),
                              nameof(MapClass1.UInt32Nullable),
                              nameof(MapClass1.UInt64),
                              nameof(MapClass1.UInt64Nullable));

            handReader.AddColumns(nameof(MapClass1.Boolean),
                             nameof(MapClass1.BooleanNullable),
                             nameof(MapClass1.Byte),
                             nameof(MapClass1.ByteNullable),
                             nameof(MapClass1.DateTime),
                             nameof(MapClass1.DateTimeNullable),
                             nameof(MapClass1.Decimal),
                             nameof(MapClass1.DecimalNullable),
                             nameof(MapClass1.Double),
                             nameof(MapClass1.DoubleNullable),
                             nameof(MapClass1.Float),
                             nameof(MapClass1.FloatNullable),
                             nameof(MapClass1.Guid),
                             nameof(MapClass1.GuidNullable),
                             nameof(MapClass1.Int16),
                             nameof(MapClass1.Int16Nullable),
                             nameof(MapClass1.Int32),
                             nameof(MapClass1.Int32Nullable),
                             nameof(MapClass1.Int64),
                             nameof(MapClass1.Int64Nullable),
                             nameof(MapClass1.Object),
                             nameof(MapClass1.SByte),
                             nameof(MapClass1.SByteNullable),
                             nameof(MapClass1.String),
                             nameof(MapClass1.UInt16),
                             nameof(MapClass1.UInt16Nullable),
                             nameof(MapClass1.UInt32),
                             nameof(MapClass1.UInt32Nullable),
                             nameof(MapClass1.UInt64),
                             nameof(MapClass1.UInt64Nullable));

            for (int i = 0; i < 100001; i++)
            {
                gmapperReader.AddValues(false,
                                 (bool?)true,
                                 (byte)10,
                                 (byte?)20,
                                 new DateTime(2001, 01, 02, 05, 30, 30),
                                 (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                                 (decimal)999999,
                                 (decimal?)55555,
                                 (double)111111,
                                 (double?)22222,
                                 (float)10.1000,
                                 (float?)11.1111,
                                 new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                                 (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                                 (short)90,
                                 (short?)91,
                                 32767,
                                 (int?)32768,
                                 (long)234234234234,
                                 (long?)234234234,
                                 (int)int.MinValue,
                                 (sbyte)3,
                                 (sbyte?)4,
                                 "Teste String",
                                 (ushort)99,
                                 (ushort?)100,
                                 (uint)67676,
                                 (uint?)87878,
                                 (ulong)7767567567567,
                                 (ulong?)456456456456456);

                handReader.AddValues(false,
                                 (bool?)true,
                                 (byte)10,
                                 (byte?)20,
                                 new DateTime(2001, 01, 02, 05, 30, 30),
                                 (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                                 (decimal)999999,
                                 (decimal?)55555,
                                 (double)111111,
                                 (double?)22222,
                                 (float)10.1000,
                                 (float?)11.1111,
                                 new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                                 (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                                 (short)90,
                                 (short?)91,
                                 32767,
                                 (int?)32768,
                                 (long)234234234234,
                                 (long?)234234234,
                                 (int)int.MinValue,
                                 (sbyte)3,
                                 (sbyte?)4,
                                 "Teste String",
                                 (ushort)99,
                                 (ushort?)100,
                                 (uint)67676,
                                 (uint?)87878,
                                 (ulong)7767567567567,
                                 (ulong?)456456456456456);
            }

            //Gera mapper e JIT Compila
            MapClass1 dummy = service.Map<MapClass1>(MapName, gmapperReader);
            MapClass1 dummy2 = MapSingleClass1(handReader);

            TimeSpan gMapperElapsed;
            TimeSpan handElapsed;

            Stopwatch watch = Stopwatch.StartNew();

            IEnumerable<MapClass1> gResult = service.MapCollection<MapClass1>(MapName, gmapperReader);

            watch.Stop();
            gMapperElapsed = watch.Elapsed;

            watch.Restart();

            IEnumerable<MapClass1> hResult = MapCollection(handReader);

            watch.Stop();
            handElapsed = watch.Elapsed;

            Assert.AreEqual(gResult.Count(), 100000);
            Assert.AreEqual(hResult.Count(), 100000);

            Debug.WriteLine("FasterThenHandCodeMappingForLargeCollections - Gmapper: " + gMapperElapsed);
            Debug.WriteLine("FasterThenHandCodeMappingForLargeCollections - Hand: " + handElapsed);
            Assert.IsTrue(gMapperElapsed < handElapsed);
        }

        [TestMethod]
        [TestCategory("Performance")]
        public void FasterThenHandCodeMappingForSmallCollections()
        {
            MemoryDataReader gmapperReader = new MemoryDataReader();
            MemoryDataReader handReader = new MemoryDataReader();

            gmapperReader.AddColumns(nameof(MapClass1.Boolean),
                              nameof(MapClass1.BooleanNullable),
                              nameof(MapClass1.Byte),
                              nameof(MapClass1.ByteNullable),
                              nameof(MapClass1.DateTime),
                              nameof(MapClass1.DateTimeNullable),
                              nameof(MapClass1.Decimal),
                              nameof(MapClass1.DecimalNullable),
                              nameof(MapClass1.Double),
                              nameof(MapClass1.DoubleNullable),
                              nameof(MapClass1.Float),
                              nameof(MapClass1.FloatNullable),
                              nameof(MapClass1.Guid),
                              nameof(MapClass1.GuidNullable),
                              nameof(MapClass1.Int16),
                              nameof(MapClass1.Int16Nullable),
                              nameof(MapClass1.Int32),
                              nameof(MapClass1.Int32Nullable),
                              nameof(MapClass1.Int64),
                              nameof(MapClass1.Int64Nullable),
                              nameof(MapClass1.Object),
                              nameof(MapClass1.SByte),
                              nameof(MapClass1.SByteNullable),
                              nameof(MapClass1.String),
                              nameof(MapClass1.UInt16),
                              nameof(MapClass1.UInt16Nullable),
                              nameof(MapClass1.UInt32),
                              nameof(MapClass1.UInt32Nullable),
                              nameof(MapClass1.UInt64),
                              nameof(MapClass1.UInt64Nullable));

            handReader.AddColumns(nameof(MapClass1.Boolean),
                             nameof(MapClass1.BooleanNullable),
                             nameof(MapClass1.Byte),
                             nameof(MapClass1.ByteNullable),
                             nameof(MapClass1.DateTime),
                             nameof(MapClass1.DateTimeNullable),
                             nameof(MapClass1.Decimal),
                             nameof(MapClass1.DecimalNullable),
                             nameof(MapClass1.Double),
                             nameof(MapClass1.DoubleNullable),
                             nameof(MapClass1.Float),
                             nameof(MapClass1.FloatNullable),
                             nameof(MapClass1.Guid),
                             nameof(MapClass1.GuidNullable),
                             nameof(MapClass1.Int16),
                             nameof(MapClass1.Int16Nullable),
                             nameof(MapClass1.Int32),
                             nameof(MapClass1.Int32Nullable),
                             nameof(MapClass1.Int64),
                             nameof(MapClass1.Int64Nullable),
                             nameof(MapClass1.Object),
                             nameof(MapClass1.SByte),
                             nameof(MapClass1.SByteNullable),
                             nameof(MapClass1.String),
                             nameof(MapClass1.UInt16),
                             nameof(MapClass1.UInt16Nullable),
                             nameof(MapClass1.UInt32),
                             nameof(MapClass1.UInt32Nullable),
                             nameof(MapClass1.UInt64),
                             nameof(MapClass1.UInt64Nullable));

            for (int i = 0; i < 21; i++)
            {
                gmapperReader.AddValues(false,
                                 (bool?)true,
                                 (byte)10,
                                 (byte?)20,
                                 new DateTime(2001, 01, 02, 05, 30, 30),
                                 (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                                 (decimal)999999,
                                 (decimal?)55555,
                                 (double)111111,
                                 (double?)22222,
                                 (float)10.1000,
                                 (float?)11.1111,
                                 new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                                 (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                                 (short)90,
                                 (short?)91,
                                 32767,
                                 (int?)32768,
                                 (long)234234234234,
                                 (long?)234234234,
                                 (int)int.MinValue,
                                 (sbyte)3,
                                 (sbyte?)4,
                                 "Teste String",
                                 (ushort)99,
                                 (ushort?)100,
                                 (uint)67676,
                                 (uint?)87878,
                                 (ulong)7767567567567,
                                 (ulong?)456456456456456);

                handReader.AddValues(false,
                                 (bool?)true,
                                 (byte)10,
                                 (byte?)20,
                                 new DateTime(2001, 01, 02, 05, 30, 30),
                                 (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                                 (decimal)999999,
                                 (decimal?)55555,
                                 (double)111111,
                                 (double?)22222,
                                 (float)10.1000,
                                 (float?)11.1111,
                                 new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                                 (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                                 (short)90,
                                 (short?)91,
                                 32767,
                                 (int?)32768,
                                 (long)234234234234,
                                 (long?)234234234,
                                 (int)int.MinValue,
                                 (sbyte)3,
                                 (sbyte?)4,
                                 "Teste String",
                                 (ushort)99,
                                 (ushort?)100,
                                 (uint)67676,
                                 (uint?)87878,
                                 (ulong)7767567567567,
                                 (ulong?)456456456456456);
            }

            //Gera mapper e JIT Compila
            MapClass1 dummy = service.Map<MapClass1>(MapName, gmapperReader);
            MapClass1 dummy2 = MapSingleClass1(handReader);

            TimeSpan gMapperElapsed;
            TimeSpan handElapsed;

            Stopwatch watch = Stopwatch.StartNew();

            IEnumerable<MapClass1> hResult = MapCollection(handReader);

            watch.Stop();
            handElapsed = watch.Elapsed;

            watch.Restart();

            IEnumerable<MapClass1> gResult = service.MapCollection<MapClass1>(MapName, gmapperReader);

            watch.Stop();
            gMapperElapsed = watch.Elapsed;

            Assert.AreEqual(gResult.Count(), 20);
            Assert.AreEqual(hResult.Count(), 20);

            Debug.WriteLine("FasterThenHandCodeMappingForSmallCollections - Gmapper: " + gMapperElapsed);
            Debug.WriteLine("FasterThenHandCodeMappingForSmallCollections - Hand: " + handElapsed);
            Assert.IsTrue(gMapperElapsed < handElapsed);
        }

        [TestMethod]
        [TestCategory("Performance")]
        public void FasterThenHandCodeMappingForSingleObject()
        {
            MemoryDataReader gmapperReader = new MemoryDataReader();
            MemoryDataReader handReader = new MemoryDataReader();

            gmapperReader.AddColumns(nameof(MapClass1.Boolean),
                              nameof(MapClass1.BooleanNullable),
                              nameof(MapClass1.Byte),
                              nameof(MapClass1.ByteNullable),
                              nameof(MapClass1.DateTime),
                              nameof(MapClass1.DateTimeNullable),
                              nameof(MapClass1.Decimal),
                              nameof(MapClass1.DecimalNullable),
                              nameof(MapClass1.Double),
                              nameof(MapClass1.DoubleNullable),
                              nameof(MapClass1.Float),
                              nameof(MapClass1.FloatNullable),
                              nameof(MapClass1.Guid),
                              nameof(MapClass1.GuidNullable),
                              nameof(MapClass1.Int16),
                              nameof(MapClass1.Int16Nullable),
                              nameof(MapClass1.Int32),
                              nameof(MapClass1.Int32Nullable),
                              nameof(MapClass1.Int64),
                              nameof(MapClass1.Int64Nullable),
                              nameof(MapClass1.Object),
                              nameof(MapClass1.SByte),
                              nameof(MapClass1.SByteNullable),
                              nameof(MapClass1.String),
                              nameof(MapClass1.UInt16),
                              nameof(MapClass1.UInt16Nullable),
                              nameof(MapClass1.UInt32),
                              nameof(MapClass1.UInt32Nullable),
                              nameof(MapClass1.UInt64),
                              nameof(MapClass1.UInt64Nullable));

            handReader.AddColumns(nameof(MapClass1.Boolean),
                             nameof(MapClass1.BooleanNullable),
                             nameof(MapClass1.Byte),
                             nameof(MapClass1.ByteNullable),
                             nameof(MapClass1.DateTime),
                             nameof(MapClass1.DateTimeNullable),
                             nameof(MapClass1.Decimal),
                             nameof(MapClass1.DecimalNullable),
                             nameof(MapClass1.Double),
                             nameof(MapClass1.DoubleNullable),
                             nameof(MapClass1.Float),
                             nameof(MapClass1.FloatNullable),
                             nameof(MapClass1.Guid),
                             nameof(MapClass1.GuidNullable),
                             nameof(MapClass1.Int16),
                             nameof(MapClass1.Int16Nullable),
                             nameof(MapClass1.Int32),
                             nameof(MapClass1.Int32Nullable),
                             nameof(MapClass1.Int64),
                             nameof(MapClass1.Int64Nullable),
                             nameof(MapClass1.Object),
                             nameof(MapClass1.SByte),
                             nameof(MapClass1.SByteNullable),
                             nameof(MapClass1.String),
                             nameof(MapClass1.UInt16),
                             nameof(MapClass1.UInt16Nullable),
                             nameof(MapClass1.UInt32),
                             nameof(MapClass1.UInt32Nullable),
                             nameof(MapClass1.UInt64),
                             nameof(MapClass1.UInt64Nullable));

            for (int i = 0; i < 2; i++)
            {
                gmapperReader.AddValues(false,
                                 (bool?)true,
                                 (byte)10,
                                 (byte?)20,
                                 new DateTime(2001, 01, 02, 05, 30, 30),
                                 (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                                 (decimal)999999,
                                 (decimal?)55555,
                                 (double)111111,
                                 (double?)22222,
                                 (float)10.1000,
                                 (float?)11.1111,
                                 new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                                 (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                                 (short)90,
                                 (short?)91,
                                 32767,
                                 (int?)32768,
                                 (long)234234234234,
                                 (long?)234234234,
                                 (int)int.MinValue,
                                 (sbyte)3,
                                 (sbyte?)4,
                                 "Teste String",
                                 (ushort)99,
                                 (ushort?)100,
                                 (uint)67676,
                                 (uint?)87878,
                                 (ulong)7767567567567,
                                 (ulong?)456456456456456);

                handReader.AddValues(false,
                                 (bool?)true,
                                 (byte)10,
                                 (byte?)20,
                                 new DateTime(2001, 01, 02, 05, 30, 30),
                                 (DateTime?)new DateTime(2001, 01, 02, 05, 31, 31),
                                 (decimal)999999,
                                 (decimal?)55555,
                                 (double)111111,
                                 (double?)22222,
                                 (float)10.1000,
                                 (float?)11.1111,
                                 new Guid("e39d831f-94a7-4fff-a0c3-fa662208a0b0"),
                                 (Guid?)new Guid("f124bfe2-1e12-4a2e-9c09-5edbd3a64f39"),
                                 (short)90,
                                 (short?)91,
                                 32767,
                                 (int?)32768,
                                 (long)234234234234,
                                 (long?)234234234,
                                 (int)int.MinValue,
                                 (sbyte)3,
                                 (sbyte?)4,
                                 "Teste String",
                                 (ushort)99,
                                 (ushort?)100,
                                 (uint)67676,
                                 (uint?)87878,
                                 (ulong)7767567567567,
                                 (ulong?)456456456456456);
            }

            //Gera mapper e JIT Compila
            MapClass1 dummy = service.Map<MapClass1>(MapName, gmapperReader);
            MapClass1 dummy2 = MapSingleClass1(handReader);

            TimeSpan gMapperElapsed;
            TimeSpan handElapsed;

            Stopwatch watch = Stopwatch.StartNew();

            MapClass1 hResult = MapSingleClass1(handReader);

            watch.Stop();
            handElapsed = watch.Elapsed;

            watch.Restart();

            MapClass1 gResult = service.Map<MapClass1>(MapName, gmapperReader);

            watch.Stop();
            gMapperElapsed = watch.Elapsed;

            Debug.WriteLine("FasterThenHandCodeMappingForSingleObject - Gmapper: " + gMapperElapsed);
            Debug.WriteLine("FasterThenHandCodeMappingForSingleObject - Hand: " + handElapsed);
            Assert.IsTrue(gMapperElapsed < handElapsed);
        }

        private IEnumerable<MapClass1> MapCollection(IDataReader reader)
        {
            List<MapClass1> result = new List<MapClass1>();

            while (reader.Read())
            {
                result.Add(MapperClass1(reader));
            }

            return result;
        }

        private MapClass1 MapSingleClass1(IDataReader reader)
        {
            if (reader.Read())
            {
                return MapperClass1(reader);
            }
            return null;
        }

        private MapClass1 MapperClass1(IDataReader reader)
        {
            return new MapClass1()
            {
                Boolean = reader.GetValue<bool>("Boolean"),
                BooleanNullable = reader.GetValue<bool?>("BooleanNullable"),
                Byte = reader.GetValue<byte>("Byte"),
                ByteNullable = reader.GetValue<byte?>("ByteNullable"),
                DateTime = reader.GetValue<DateTime>("DateTime"),
                DateTimeNullable = reader.GetValue<DateTime?>("DateTimeNullable"),
                Decimal = reader.GetValue<decimal>("Decimal"),
                DecimalNullable = reader.GetValue<decimal?>("DecimalNullable"),
                Double = reader.GetValue<double>("Double"),
                DoubleNullable = reader.GetValue<double?>("DoubleNullable"),
                Float = reader.GetValue<float>("Float"),
                FloatNullable = reader.GetValue<float?>("FloatNullable"),
                Guid = reader.GetValue<Guid>("Guid"),
                GuidNullable = reader.GetValue<Guid?>("GuidNullable"),
                Int16 = reader.GetValue<short>("Int16"),
                Int16Nullable = reader.GetValue<short?>("Int16Nullable"),
                Int32 = reader.GetValue<int>("Int32"),
                Int32Nullable = reader.GetValue<int?>("Int32Nullable"),
                Int64 = reader.GetValue<long>("Int64"),
                Int64Nullable = reader.GetValue<long?>("Int64Nullable"),
                Object = reader.GetValue<int>("Object"),
                SByte = reader.GetValue<sbyte>("SByte"),
                SByteNullable = reader.GetValue<sbyte?>("SByteNullable"),
                String = reader.GetValue<string>("String"),
                UInt16 = reader.GetValue<ushort>("UInt16"),
                UInt16Nullable = reader.GetValue<ushort?>("UInt16Nullable"),
                UInt32 = reader.GetValue<uint>("UInt32"),
                UInt32Nullable = reader.GetValue<uint?>("UInt32Nullable"),
                UInt64 = reader.GetValue<ulong>("UInt64"),
                UInt64Nullable = reader.GetValue<ulong?>("UInt64Nullable")
            };
        }
    }
}
