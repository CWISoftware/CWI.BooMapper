# CWI.BooMapper

BooMapper é uma biblioteca que você pode utilizar no seu projeto para facilitar a obtenção e o mapeamento de dados
de um banco de dados relacional sem perda de performance, na verdade com ganho !

## Funcionalidades

### Mapeamento
A biblioteca expõem publicamente a interface IRelationalMapperService, que contém os métodos de mapeamento.
O mapeamento funciona informando uma instância de IDataReader e um um tipo, que será o objeto mapeado.
O BooMapper requer que o nome das colunas no IDataReader sejam o mesmo nome das propriedades do objeto mapeado.

```csharp
public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
}            

IDataReader reader = null; //SELECT Id, Nome FROM Usuarios;
BasicRelationalMapperService service = new BasicRelationalMapperService();
Usuario usuario = service.Map<Usuario>("SingleUsuarioMapper", reader);

```

### Mapeamento profundo

O BooMapper suporta propriedades que são outros objetos, utilizando a sintaxe do C#, 
e consegue mapea-las automaticamente. O nível máximo de profundidade recomendado é de 3 objetos.

Considere o código abaixo
```csharp
   public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public Perfil Perfil { get; set; }
    }

    public class Perfil
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }

```
O mapeamento para o select

```sql
SELECT u.Id, u.Nome, p.Id as "Perfil.Id", p.Nome as "Perfil.Nome" FROM Usuarios u
INNER JOIN
  UsuarioPerfil p 
ON
  p.UsuarioId = u.Id;
```

Produz um objeto Usuario com Id, Nome, Perfil com Id e nome.

### Arquitetura

A biblioteca fornece um modelo de arquitetura para Repository Pattern.
A classe BaseCommand fornece metódos básicos para acesso a um banco de dados relacional com um encapsulamento
orientado a objetos.

```csharp
internal class ObterUsuarioCommand : MySqlTextCommand
    {
        private const string SQL = "SELECT Id,Nome FROM USUARIOS WHERE Id = @Id";

        public ObterUsuarioCommand(IDatabase database, int id)
            : base(SQL, database)
        {
            Parameters.Add(new MySqlParameter("Id", MySqlDbType.Int32)
            {
                Value = id
            });
        }
    }

IDatabase database = new MySqlDatabase(new MySqlConnection("CS"));
BasicRelationalMapperService mService = new BasicRelationalMapperService();
return new ObterUsuarioCommand(database, 10).ExecuteMapper<Usuario>("UsuarioMapper",mService);
```

`IDatabase` representa um "wrapper" de um banco de dados, contém a conexão e a transação atual
e é desenvolvido para ser utilizado num padrão de unidade de trabalho.

`BaseCommand` pode ser implementado para qualquer banco de dados e é um "wrapper" em cima de um DbCommand
e de uma funcionalidade específica de banco de dados (obter um usuário no exemplo acima).

A vantagem desse modelo é manter encapsulado as regras de acesso aos objetos de um domínio, facilitando o
entendendimento e a manutenção. Os Commands também podem ser utilizados com Stored Procedures.

## Performance

O BooMapper só utiliza reflection uma vez por chave, ele obtém as propriedades do objeto e emite código IL
em runtime que faz o mapeamento. Todas as chamadas após a primeira já utilizam a função compilada.
Pelo fato de emitir o IL em runtime, chamadas de métodos que seriam necessárias para converter valores
são feitas "inline", diminuindo esse custo, assim ele consegue ser mais rápido que um mapeamento feito manualmente.
A service disponível recebe um parâmetro chamado "key". Esse parâmetro identifica unicamente cada mapper,
é assim que os mapper's são identificados e reaproveitados.

## Limitações e considerações

- O BooMapper foi desenvolvido para ser um Mapper "straightforward", quer dizer,
os nomes e tipos de propriedades do IDataReader devem atender a classe sendo mapeada
para que os cast's sejam válidos. Ele basicamente faz um mapeamento direto e reto do IDataReader para a classe.

- As propriedades que vão ser mapeadas não precisam ser public set, podendo ser private, internal , protected ou private,
mas é necessário ter declarado o método set.

- A mesma regra vale para construtores, todas as classes que serão mapeadas devem possuir um construtor sem parâmetros,
podendo ser private, public, internal ou protected.
