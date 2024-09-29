using SftpFileDownloader.Core.Models;

namespace SftpFileDownloader.Core.Interfaces
{
    /// <summary>
    /// Interface que define operações de download e listagem de arquivos via SFTP.
    /// </summary>
    public interface IClienteSftp
    {
        /// <summary>
        /// Baixa o arquivo mais recente do diretório remoto para o diretório local.
        /// </summary>
        /// <param name="diretorioRemoto">Caminho do diretório remoto.</param>
        /// <param name="diretorioLocal">Caminho do diretório local onde o arquivo será salvo.</param>
        void BaixarArquivoMaisRecente(string diretorioRemoto, string diretorioLocal);

        /// <summary>
        /// Lista todos os arquivos no diretório remoto com detalhes.
        /// </summary>
        /// <param name="diretorioRemoto">Caminho do diretório remoto.</param>
        /// <returns>Uma lista de objetos SftpFileInfo contendo detalhes dos arquivos.</returns>
        List<DetalhesArquivoSftp> ListarArquivosComDetalhes(string diretorioRemoto);

        /// <summary>
        /// Baixa um arquivo pelo nome.
        /// </summary>
        /// <param name="diretorioRemoto">Caminho do diretório remoto.</param>
        /// <param name="nomeArquivo">Nome do arquivo a ser baixado.</param>
        /// <param name="diretorioLocal">Caminho do diretório local onde o arquivo será salvo.</param>
        void BaixarArquivoPorNome(string diretorioRemoto, string nomeArquivo, string diretorioLocal);
    }
}
