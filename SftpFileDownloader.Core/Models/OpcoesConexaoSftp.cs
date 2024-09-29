using WinSCP;

namespace SftpFileDownloader.Core.Models
{
    /// <summary>
    /// Representa as configurações para conexão SFTP.
    /// </summary>
    public class OpcoesConexaoSftp
    {
        public Protocol Protocolo { get; set; } = Protocol.Sftp;
        public string NomeHost { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public string ImpressaoDigitalChaveSsh { get; set; }
    }
}
