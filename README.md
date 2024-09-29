
# Componente SFTP para .NET

Este componente permite a integração com servidores SFTP, permitindo baixar arquivos, listar arquivos e recuperar detalhes de arquivos diretamente do servidor remoto. Utilizando boas práticas como Clean Code, Injeção de Dependência e utilizando o WinSCP.

## Funcionalidades Principais

- **Baixar o arquivo mais recente**: Baixa automaticamente o arquivo mais recente do diretório remoto.
- **Listar arquivos com detalhes**: Retorna uma lista de arquivos no diretório remoto, com informações sobre nome, tamanho e data de modificação.
- **Baixar um arquivo específico por nome**: Baixa um arquivo específico com base no nome fornecido pelo usuário.

## Estrutura do Projeto

O componente é dividido nas seguintes partes principais:

### 1. `OpcoesConexaoSftp`
Define as opções de conexão com o servidor SFTP, como nome do host, nome de usuário, senha e a impressão digital da chave SSH.

#### Exemplo de Uso:
```csharp
var opcoes = new OpcoesConexaoSftp
{
    NomeHost = "example.com",
    NomeUsuario = "usuario",
    Senha = "senha",
    ImpressaoDigitalChaveSsh = "ssh-rsa 2048 xxxxx..."
};
```

### 2. `IClienteSftp`
Interface que define os métodos que podem ser utilizados para interação com o servidor SFTP.

- **BaixarArquivoMaisRecente**: Baixa o arquivo mais recente do diretório remoto.
- **ListarArquivosComDetalhes**: Retorna uma lista de detalhes dos arquivos presentes no diretório remoto.
- **BaixarArquivoPorNome**: Baixa um arquivo específico por nome.

### 3. `ClienteSftp`
Implementa a interface `IClienteSftp`, contendo a lógica para interação com o servidor SFTP.

#### Exemplo de Uso:
```csharp
var clienteSftp = new ClienteSftp(opcoes);
clienteSftp.BaixarArquivoMaisRecente("/home/user", "C:/downloads");
```

## Instalação

1. Adicione a referência ao pacote `WinSCP` via NuGet:
   ```
   Install-Package WinSCP
   ```

2. Adicione o componente SFTP ao seu projeto.

### Injeção de Dependências

O componente pode ser facilmente integrado utilizando Injeção de Dependências.

#### Exemplo de Configuração:

```csharp
public static class ConfiguraDependenciasSftp
{
    public static void AdicionarServicosSftp(this IServiceCollection servicos)
    {
        servicos.AddSingleton<OpcoesConexaoSftp>(provedor =>
        {
            return new OpcoesConexaoSftp
            {
                NomeHost = "example.com",
                NomeUsuario = "usuario",
                Senha = "senha",
                ImpressaoDigitalChaveSsh = "ssh-rsa 2048 xxxxx..."
            };
        });

        servicos.AddTransient<IClienteSftp, ClienteSftp>();
    }
}
```

No `Program.cs`:

```csharp
var serviceProvider = new ServiceCollection()
    .AdicionarServicosSftp()
    .BuildServiceProvider();

var clienteSftp = serviceProvider.GetService<IClienteSftp>();
```

## Testes

O componente foi desenvolvido com testes automatizados utilizando `Moq` e `Shouldly` para garantir a qualidade e o correto funcionamento das funcionalidades.

### Exemplo de Testes:

#### Teste: Baixar o Arquivo Mais Recente
```csharp
[Fact]
public void BaixarArquivoMaisRecente_DeveBaixarComSucesso()
{
    _clienteSftpMock.Setup(cliente => cliente.BaixarArquivoMaisRecente(It.IsAny<string>(), It.IsAny<string>()))
                    .Verifiable();

    _clienteSftpMock.Object.BaixarArquivoMaisRecente(It.IsAny<string>(), It.IsAny<string>());

    _clienteSftpMock.Verify(cliente => cliente.BaixarArquivoMaisRecente(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    _clienteSftpMock.Invocations.Count.ShouldBe(1);
}
```

### Teste: Listar Arquivos com Detalhes
```csharp
[Fact]
public void ListarArquivosComDetalhes_DeveRetornarListaDeArquivos()
{
    var listaMockDeArquivos = new List<DetalhesArquivoSftp>
    {
        new DetalhesArquivoSftp { NomeArquivo = "arquivo1.txt", Tamanho = 1000, UltimaModificacao = DateTime.Now },
        new DetalhesArquivoSftp { NomeArquivo = "arquivo2.txt", Tamanho = 2000, UltimaModificacao = DateTime.Now }
    };

    _clienteSftpMock.Setup(cliente => cliente.ListarArquivosComDetalhes(It.IsAny<string>()))
                    .Returns(listaMockDeArquivos);

    var resultado = _clienteSftpMock.Object.ListarArquivosComDetalhes(It.IsAny<string>());

    resultado.Count.ShouldBe(2);
    resultado[0].NomeArquivo.ShouldBe("arquivo1.txt");
    resultado[1].NomeArquivo.ShouldBe("arquivo2.txt");
}
```

### Estrutura de Testes

Os testes estão organizados para verificar o comportamento dos métodos principais, como o download de arquivos e a listagem de arquivos. Utilizamos **Shouldly** para garantir que as asserções sejam mais legíveis.

## Contribuições

Contribuições são bem-vindas! Para contribuir:

1. Faça um fork do repositório.
2. Crie uma nova branch (`git checkout -b feature/nova-funcionalidade`).
3. Faça suas modificações e crie commits (`git commit -am 'Adiciona nova funcionalidade'`).
4. Envie para o branch (`git push origin feature/nova-funcionalidade`).
5. Abra um Pull Request.

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
