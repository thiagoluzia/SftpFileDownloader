
# Componente SFTP para .NET

Este componente permite a integração com servidores SFTP, permitindo baixar arquivos, listar arquivos e recuperar detalhes de arquivos diretamente do servidor remoto. Utilizando o WinSCP.

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
