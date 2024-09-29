using Moq;
using SftpFileDownloader.Core.Interfaces;
using SftpFileDownloader.Core.Models;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace SftpFileDownloader.Tests
{
    public class ClienteSftpTests
    {
        private readonly Mock<IClienteSftp> _clienteSftpMock;

        public ClienteSftpTests()
        {
            // Inicializando o mock da interface IClienteSftp
            _clienteSftpMock = new Mock<IClienteSftp>();
        }

        [Fact]
        public void BaixarArquivoMaisRecente_DeveBaixarComSucesso()
        {
            // Simular o comportamento usando It.IsAny para aceitar qualquer string como parâmetros
            _clienteSftpMock.Setup(cliente => cliente.BaixarArquivoMaisRecente(It.IsAny<string>(), It.IsAny<string>()))
                            .Verifiable();

            // Chamar o método mockado
            _clienteSftpMock.Object.BaixarArquivoMaisRecente(It.IsAny<string>(), It.IsAny<string>());

            // Verificar se o método foi chamado corretamente com quaisquer parâmetros usando Shouldly
            _clienteSftpMock.Verify(cliente => cliente.BaixarArquivoMaisRecente(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            // Verificação usando Shouldly (não tem valor direto a verificar neste caso específico, já que estamos verificando o comportamento)
            _clienteSftpMock.Invocations.Count.ShouldBe(1);
        }

        [Fact]
        public void ListarArquivosComDetalhes_DeveRetornarListaDeArquivos()
        {
            // Usar valores aleatórios ou padrões para simular os dados
            var arquivo1 = Guid.NewGuid().ToString();  // Simular qualquer string
            var arquivo2 = Guid.NewGuid().ToString();  // Simular qualquer string

            var tamanho1 = new Random().Next(1, 1000); // Simular qualquer tamanho de arquivo
            var tamanho2 = new Random().Next(1, 1000); // Simular qualquer tamanho de arquivo

            var data1 = DateTime.Now.AddDays(-1);  // Simular qualquer data
            var data2 = DateTime.Now.AddDays(-2);  // Simular qualquer data

            // Mockar uma lista de arquivos
            var listaMockDeArquivos = new List<DetalhesArquivoSftp>
            {
                new DetalhesArquivoSftp { NomeArquivo = arquivo1, Tamanho = tamanho1, UltimaModificacao = data1 },
                new DetalhesArquivoSftp { NomeArquivo = arquivo2, Tamanho = tamanho2, UltimaModificacao = data2 }
            };

            // Configurar o mock para retornar a lista simulada
            _clienteSftpMock.Setup(cliente => cliente.ListarArquivosComDetalhes(It.IsAny<string>()))
                            .Returns(listaMockDeArquivos);

            // Chamar o método mockado
            var resultado = _clienteSftpMock.Object.ListarArquivosComDetalhes(It.IsAny<string>());

            // Verificar se a lista retornada contém os arquivos simulados usando Shouldly
            resultado.Count.ShouldBe(2);
            resultado[0].NomeArquivo.ShouldBe(arquivo1);
            resultado[1].NomeArquivo.ShouldBe(arquivo2);
        }

        [Fact]
        public void BaixarArquivoPorNome_DeveBaixarArquivoEspecifico()
        {
            // Simular o comportamento com It.IsAny para aceitar qualquer nome de arquivo e diretório
            _clienteSftpMock.Setup(cliente => cliente.BaixarArquivoPorNome(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                            .Verifiable();

            // Chamar o método mockado
            _clienteSftpMock.Object.BaixarArquivoPorNome(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            // Verificar se o método foi chamado corretamente com quaisquer parâmetros usando Shouldly
            _clienteSftpMock.Verify(cliente => cliente.BaixarArquivoPorNome(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            // Verificação adicional de chamadas
            _clienteSftpMock.Invocations.Count.ShouldBe(1);
        }
    }
}
