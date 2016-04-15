using Sigil;
using System;
using System.Reflection;
using CWI.BooMapper.Core.Extensions;

namespace CWI.BooMapper.Core.Oto
{
    internal class OtoMapBuilder<TIn, TOut> : OtoMapBuilder where TIn : class 
                                                            where TOut : class
    {
        private readonly Type destType;
        private readonly Type targetType;

        public OtoMapBuilder()
        {
            destType = typeof(TOut);
            targetType = typeof(TIn);
        }

        public OtoMapper<TIn, TOut> Generate(TIn target, string key)
        {
            Emit<OtoMapper<TIn, TOut>> il = Emit<OtoMapper<TIn, TOut>>.NewDynamicMethod(typeof(OtoMapBuilder<TIn, TOut>), $"OTOMapper_{key}");

            Local retorno = il.DeclareLocal(destType); //var retorno; (0)

            //retorno = new()
            il.NewObject(destType.GetParameterlessConstructor()); //Pilha = [retorno]
            il.StoreLocal(retorno); //Pilha = []

            Label isNullLabel = il.DefineLabel();
            il.LoadArgument(0); //Pilha = [target]
            il.LoadNull(); //Pilha = [target][null]

            il.BranchIfEqual(isNullLabel); //Pilha = []

            foreach (PropertyInfo targetProp in targetType.GetWriteableInstanceProperties())
            {
                PropertyInfo destProp = destType.GetWriteableInstanceProperty(targetProp.Name);

                if (destProp != null)
                {
                    Type targetPropType = targetProp.PropertyType;
                    Type destPropType = destProp.PropertyType;

                    //As duas propriedades são ienumerable na essencia.
                    if (!targetPropType.IsValueTypeOrString() &&
                        !destPropType.IsValueTypeOrString() &&
                        targetPropType.IsEnumerable() &&
                        destPropType.IsEnumerable())
                    {
                        //As duas coleções são do mesmo tipo
                        //string[] -> string[]
                        //string[] -> IEnumerable<string>
                        if (targetPropType.GetTypeOrUnderlyingType() == destPropType.GetTypeOrUnderlyingType())
                        {
                            il.LoadLocal(retorno); //Pilha = [Retorno]
                            il.LoadArgument(0); //Pilha = [Retorno][Target]
                            il.CallVirtual(targetProp.GetGetMethod(true)); //Pilha = [Retorno][Collection]

                            MethodInfo copyMethod = Methods.GetCopyMethod(destPropType);

                            if (copyMethod != null)
                            {
                                il.Call(copyMethod.ResolveGenericMethod(destPropType.GetTypeOrUnderlyingType())); //Pilha = [Retorno][Collection-Copy]
                            }
                            else
                            {
                                throw new NotImplementedException($"O tipo {destPropType.FullName} não é implementado.");
                            }

                            il.CallVirtual(destProp.GetSetMethod(true)); //Pilha = []
                        }
                    }

                    //As duas propriedades são do mesmo tipo
                    else if (targetPropType == destPropType)
                    {
                        il.LoadLocal(retorno); //Pilha = [Retorno]
                        il.LoadArgument(0); //Pilha = [Retorno][Target]
                        il.CallVirtual(targetProp.GetGetMethod(true)); //Pilha = [Retorno][Value]
                        il.CallVirtual(destProp.GetSetMethod(true)); //Pilha = []
                    }

                    //As duas propriedades são do mesmo tipo base
                    //int? -> int
                    //Enum -> int
                    else if(targetPropType.GetTypeOrUnderlyingType() == destPropType.GetTypeOrUnderlyingType())
                    {
                        il.LoadLocal(retorno); //Pilha = [Retorno]
                        il.LoadArgument(0); //Pilha = [Retorno][Target]
                        il.CallVirtual(targetProp.GetGetMethod(true)); //Pilha = [Retorno][Value]

                        //Fonte é nullable mas destino não, nesse caso chama o GetValueOrDefault no nullable.
                        if(targetPropType.IsNullable() && !destPropType.IsNullable())
                        {
                            il.Call(Methods.OTOMapperBuilder_GetNullableValue.ResolveGenericMethod(targetPropType.GetTypeOrUnderlyingType())); //Pilha = [Retorno][Value-Value]
                        }

                        //Para tipos numéricos diferentes ou enums
                        //int -> short
                        //double -> float
                        //Enum -> int
                        //int -> enum
                        else if(targetPropType.IsValueType && destPropType.IsValueType)
                        {
                            il.Convert(destPropType.GetTypeOrUnderlyingType()); //Pilha = [Retorno][Value-Convertido]
                        }

                        il.CallVirtual(destProp.GetSetMethod(true)); //Pilha = []
                    }
                }
            }

            il.LoadLocal(retorno);
            il.Return();

            il.MarkLabel(isNullLabel);
            il.LoadNull();
            il.Return();

            return il.CreateDelegate();
        }
    }
}
