using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CWI.BooMapper.Core.Oto;

namespace CWI.BooMapper.Tests
{
    [TestClass]
    public class OtoMapperTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Teste xxx = new Teste()
            {
                IntNullable = 10
            };

            if(xxx == null)
            {
                return;
            }

            int b = xxx.IntNullable.GetValueOrDefault();

            OtoMapBuilder<Teste, TesteDto> biu = new OtoMapBuilder<Teste, TesteDto>();

            Teste t = new Teste()
            {
                Nome = "Teste",
                Idade = 20,
                Ids = new int[]
                {
                    1,2,3,4,5,6,7,8,9
                },
                Strings = new string[]
                {
                    "a", "b", "c"
                },
                Guids = new Guid[]
                {
                    Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()
                },
                Nullables = new int?[]
                {
                    1,2 , null ,3
                },
                Sexo = Sexo.Masculino,
                SexoDto = 2
            };

            OtoMapper<Teste, TesteDto> mapper = biu.Generate(t, "bla");

            TesteDto d1 = mapper(t);
            TesteDto d2 = mapper(null);
        }
    }

    public class Teste
    {
        public string Nome { get; set; }

        public int Idade { get; set; }

        public int[] Ids { get; set; }

        public string[] Strings { get; set; }

        public Guid[] Guids { get; set; }

        public int?[] Nullables { get; set; }

        public Sexo Sexo { get; set; }

        public int SexoDto { get; set; }

        public int? IntNullable { get; set; }
    }

    public class TesteDto
    {
        public string Nome { get; set; }

        public int Idade { get; set; }

        public int[] Ids { get; set; }

        public string[] Strings { get; set; }

        public List<Guid> Guids { get; set; }

        public int?[] Nullables { get; set; }

        public int Sexo { get; set; }

        public SexoDto SexoDto { get; set; }

        public int IntNullable { get; set; }
    }

    public enum Sexo : int
    {
        Masculino = 1,
        Feminino = 2
    }

    public enum SexoDto : int
    {
        Masculno = 1,
        Feminino = 2
    }
}
