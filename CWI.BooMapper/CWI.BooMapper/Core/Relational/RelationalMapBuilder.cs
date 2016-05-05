using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using CWI.BooMapper.Core.Extensions;
using Sigil;

namespace CWI.BooMapper.Core.Relational
{
    internal class RelationalMapBuilder
    {
        private readonly Type targetType;
        private readonly PropertyStack nestedStack;

        public RelationalMapBuilder(Type targetType, PropertyStack nestedStack)
        {
            if (!targetType.IsClass || targetType == Methods.TypeOfString)
            {
                throw new ArgumentException($"{targetType.FullName} não é uma classe válida.");
            }
            this.targetType = targetType;
            this.nestedStack = nestedStack ?? new PropertyStack();
        }

        public RelationalMapBuilder(Type targetType)
            : this(targetType, null)
        {
        }

        public RelationalMapper Generate(IDataReader reader, string key)
        {
            Emit<RelationalMapper> il = Emit<RelationalMapper>.NewDynamicMethod(typeof(RelationalMapBuilder), $"Mapper_{key}");

            Local retorno = il.DeclareLocal(targetType); //var retorno; (0)

            //retorno = new()
            il.NewObject(targetType.GetParameterlessConstructor()); //Pilha = [retorno]
            il.StoreLocal(retorno); //Pilha = []

            GenerateBaseProperties(reader, il, retorno);
            GenerateNestedProperties(reader, il, retorno);

            il.LoadLocal(retorno);
            il.Return();

            return il.CreateDelegate();
        }

        private void GenerateNestedProperties(IDataReader reader, Emit<RelationalMapper> il, Local retorno)
        {
            foreach (string nestedProp in ReadNestedColumns(reader))
            {
                PropertyInfo prop = targetType.GetWriteableInstanceProperty(nestedProp);

                if (prop != null)
                {
                    string subKey = Guid.NewGuid().ToString();

                    RelationalMapBuilder gen = new RelationalMapBuilder(prop.PropertyType, nestedStack.Push(prop));
                    RelationalMapper activator = gen.Generate(reader, subKey);

                    nestedStack.Pop();

                    RelationalMapperCache.Add(subKey, activator);

                    Local mapper = il.DeclareLocal<RelationalMapper>(); //Mapper mapper (3)
                    Local novoObj = il.DeclareLocal(prop.PropertyType); //novoObj (4)
                    il.LoadConstant(subKey);

                    il.Call(Methods.MapperCache_Get);
                    il.StoreLocal(mapper);
                    il.LoadLocal(mapper); //Pilha = [mapper]

                    il.LoadArgument(0); //Pilha = [mapper][reader]

                    il.CallVirtual(Methods.Mapper_Invoke); //Pilha = [value-as-object]
                    il.CastClass(prop.PropertyType);
                    il.StoreLocal(novoObj); // Pilha = []
                    il.LoadLocal(retorno); //Pilha = [retorno]
                    il.LoadLocal(novoObj); //Pilha = [retorno][novo-obj]

                    il.CallVirtual(prop.GetSetMethod(true)); //Pilha = []
                }
            }
        }

        private void GenerateBaseProperties(IDataReader reader, Emit<RelationalMapper> il, Local retorno)
        {
            int index;

            foreach (ColumnMap column in ReadColumns(reader))
            {
                PropertyInfo prop = targetType.GetWriteableInstanceProperty(column.ParsedName);

                if (prop != null && IsMappeable(prop.PropertyType))
                {
                    index = reader.GetOrdinal(column.ReaderName);

                    ExceptionBlock tryBlock = il.BeginExceptionBlock();

                    il.LoadArgument(0); //Pilha = [reader]
                    il.LoadConstant(index); //Pilha = [reader][index]

                    //isDbNull = reader.IsDbNull(index)
                    il.CallVirtual(Methods.IDataRecord_IsDbNull); //Pilha = [bool] (is db null)

                    //if(!isDbNull)
                    Label labelDbNull = il.DefineLabel(); //Pilha = []
                    il.BranchIfTrue(labelDbNull);
                    {
                        il.LoadLocal(retorno); //Pilha = [retorno]
                        il.LoadArgument(0); //Pilha = [retorno][reader]

                        il.LoadConstant(index); //Pilha = [retorno][reader][index]
                        il.CallVirtual(Methods.IDataRecord_GetValue); //Pilha = [retorno][valor-as-object]

                        if (prop.PropertyType != Methods.TypeOfObject)
                        {
                            if (prop.PropertyType == Methods.TypeOfDateTime ||
                                prop.PropertyType == Methods.TypeOfDateTimeNullable)
                            {
                                il.Call(Methods.Convert_ToDateTime); //Pilha = [retorno][valor-as-datetime]
                            }
                            else if (prop.PropertyType == Methods.TypeOfGuid ||
                                     prop.PropertyType == Methods.TypeOfGuidNullable)
                            {
                                il.Call(Methods.RelationalMapBuilder_GetGuid); //Pilha = [retorno][valor-as-guid]
                            }
                            else
                            {
                                il.UnboxAny(prop.PropertyType.GetTypeOrUnderlyingType()); //Pilha = [retorno][valor-tipado]
                            }
                        }
                        if (prop.PropertyType.IsNullable())
                        {
                            il.NewObject(prop.PropertyType.GetNullableConstructor()); //Pilha = [retorno][valor-nullable-tipado]
                        }

                        //retorno.Propriedade = valor
                        il.CallVirtual(prop.GetSetMethod(true)); //Pilha = []
                    }
                    //else
                    {
                        il.MarkLabel(labelDbNull);
                    }

                    CatchBlock catchBlock = il.BeginCatchAllBlock(tryBlock); //Pilha = [Exception]
                    il.LoadArgument(0); //Pilha = [Exception][Reader]
                    il.LoadConstant(index); //Pilha = [Exception][Reader][Index]
                    il.Call(Methods.RelationalMapBuilder_ThrowDataException);
                    il.EndCatchBlock(catchBlock); // Pilha = []
                    il.EndExceptionBlock(tryBlock);
                }
            }
        }

        private bool IsMappeable(Type _type)
        {
            Type realType = _type.GetTypeOrUnderlyingType();

            return realType.IsValueType ||
                   realType == Methods.TypeOfObject ||
                   realType == Methods.TypeOfString;
        }

        private IEnumerable<ColumnMap> ReadColumns(IDataReader reader)
        {
            return reader.GetNames()
                         .Where(prop => prop.GetNestedLevel() == nestedStack.NestedLevel && IsPropertyMatch(prop))
                         .Select(prop => new ColumnMap(prop, ParsePropertyName(prop, nestedStack.NestedLevel)));
        }

        private IEnumerable<string> ReadNestedColumns(IDataReader reader)
        {
            return reader.GetNames()
                         .Where(prop => prop.GetNestedLevel() == nestedStack.NestedLevel + 1)
                         .Select(prop => ReadNestedProperty(prop, nestedStack.NestedLevel + 1))
                         .Distinct();
        }

        private bool IsPropertyMatch(string prop)
        {
            if (nestedStack.NestedLevel == 0)
                return true;

            return string.Equals(prop.Substring(0, prop.LastIndexOf(".", StringComparison.Ordinal)), nestedStack.ToStackString());
        }

        private string ParsePropertyName(string name, int level)
        {
            //shortcut
            if (level == 0)
            {
                return name;
            }

            int len = 0;
            int nestedCount = 0;

            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == '.')
                {
                    nestedCount++;
                }
                if (nestedCount == level)
                {
                    break;
                }
                len++;
            }

            return name.Substring(len + 1);
        }

        private string ReadNestedProperty(string name, int level)
        {
            //shortcut
            if (level == 0)
            {
                return name;
            }

            return name.Split('.')[level - 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Guid GetGuid(object obj)
        {
            if (obj is Guid)
            {
                return (Guid)obj;
            }

            string str = obj as string;

            if (str != null)
            {
                return new Guid(str);
            }
            throw new InvalidCastException($"Não foi possível converter {(obj == null ? "null" : obj.GetType().FullName)} para System.Guid.");
        }

        internal static void ThrowDataException(Exception inner, IDataReader reader, int index)
        {
            Exception toThrow = null;

            try
            {
                if (reader != null && index >= 0 && index < reader.FieldCount)
                {
                    string name = reader.GetName(index);
                    Type columnType = reader.GetFieldType(index);
                    string message = $"Tentativa de cast inválido para a coluna '{name}'. O tipo recebido foi '{columnType.FullName}'.";
                    toThrow = new DataException(message, inner);
                }
                else
                {
                    toThrow = inner ?? new DataException("Erro inesperado.", null);
                }
            }
            catch (Exception ex)
            {
                toThrow = ex;
            }
            throw toThrow;
        }
    }
}
