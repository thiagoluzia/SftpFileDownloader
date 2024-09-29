using SftpFileDownloader.Core.Interfaces;
using SftpFileDownloader.Core.Models;
using WinSCP;

namespace SftpFileDownloader.Core.Services
{
    /// <summary>
    /// Implementa operações de download e listagem de arquivos via SFTP.
    /// </summary>
    public class ClienteSftp : IClienteSftp
    {
        private readonly OpcoesConexaoSftp _opcoesConexao;

        public ClienteSftp(OpcoesConexaoSftp opcoesConexao)
        {
            _opcoesConexao = opcoesConexao ?? throw new ArgumentNullException(nameof(opcoesConexao));
        }

        /// <summary>
        /// Baixa o arquivo mais recente do diretório remoto para o diretório local.
        /// </summary>
        public void BaixarArquivoMaisRecente(string diretorioRemoto, string diretorioLocal)
        {
            try
            {
                using (Session sessao = new Session())
                {
                    sessao.Open(new SessionOptions
                    {
                        Protocol = _opcoesConexao.Protocolo,
                        HostName = _opcoesConexao.NomeHost,
                        UserName = _opcoesConexao.NomeUsuario,
                        Password = _opcoesConexao.Senha,
                        SshHostKeyFingerprint = _opcoesConexao.ImpressaoDigitalChaveSsh,
                    });

                    // Obter a lista de arquivos no diretório remoto
                    RemoteDirectoryInfo informacaoDiretorio = sessao.ListDirectory(diretorioRemoto);

                    // Selecionar o arquivo mais recente
                    RemoteFileInfo arquivoMaisRecente = informacaoDiretorio.Files
                        .Where(arquivo => !arquivo.IsDirectory)
                        .OrderByDescending(arquivo => arquivo.LastWriteTime)
                        .FirstOrDefault();

                    if (arquivoMaisRecente == null)
                    {
                        throw new FileNotFoundException("Nenhum arquivo encontrado no diretório remoto.");
                    }

                    // Baixar o arquivo mais recente
                    sessao.GetFileToDirectory(arquivoMaisRecente.FullName, diretorioLocal);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao baixar o arquivo mais recente. Detalhes: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lista todos os arquivos no diretório remoto com detalhes.
        /// </summary>
        public List<DetalhesArquivoSftp> ListarArquivosComDetalhes(string diretorioRemoto)
        {
            try
            {
                using (Session sessao = new Session())
                {
                    sessao.Open(new SessionOptions
                    {
                        Protocol = _opcoesConexao.Protocolo,
                        HostName = _opcoesConexao.NomeHost,
                        UserName = _opcoesConexao.NomeUsuario,
                        Password = _opcoesConexao.Senha,
                        SshHostKeyFingerprint = _opcoesConexao.ImpressaoDigitalChaveSsh,
                    });

                    // Obter a lista de arquivos no diretório remoto
                    RemoteDirectoryInfo informacaoDiretorio = sessao.ListDirectory(diretorioRemoto);

                    // Mapear os arquivos para DetalhesArquivoSftp
                    return informacaoDiretorio.Files
                        .Where(arquivo => !arquivo.IsDirectory)
                        .Select(arquivo => new DetalhesArquivoSftp
                        {
                            NomeArquivo = arquivo.Name,
                            Tamanho = arquivo.Length,
                            UltimaModificacao = arquivo.LastWriteTime
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao listar arquivos no diretório {diretorioRemoto}. Detalhes: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Baixa um arquivo pelo nome.
        /// </summary>
        public void BaixarArquivoPorNome(string diretorioRemoto, string nomeArquivo, string diretorioLocal)
        {
            try
            {
                using (Session sessao = new Session())
                {
                    sessao.Open(new SessionOptions
                    {
                        Protocol = _opcoesConexao.Protocolo,
                        HostName = _opcoesConexao.NomeHost,
                        UserName = _opcoesConexao.NomeUsuario,
                        Password = _opcoesConexao.Senha,
                        SshHostKeyFingerprint = _opcoesConexao.ImpressaoDigitalChaveSsh,
                    });

                    // Combinar o nome do arquivo com o caminho do diretório remoto
                    string caminhoArquivoRemoto = $"{diretorioRemoto}/{nomeArquivo}";

                    // Baixar o arquivo especificado
                    sessao.GetFileToDirectory(caminhoArquivoRemoto, diretorioLocal);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao baixar o arquivo {nomeArquivo}. Detalhes: {ex.Message}", ex);
            }
        }
    }
}
