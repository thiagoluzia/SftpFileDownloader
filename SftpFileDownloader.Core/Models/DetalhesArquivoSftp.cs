namespace SftpFileDownloader.Core.Models
{
    /// <summary>
    /// Representa os detalhes de um arquivo no servidor SFTP.
    /// </summary>
    public class DetalhesArquivoSftp
    {
        public string NomeArquivo { get; set; }
        public long Tamanho { get; set; }
        public DateTime UltimaModificacao { get; set; }
    }
}
