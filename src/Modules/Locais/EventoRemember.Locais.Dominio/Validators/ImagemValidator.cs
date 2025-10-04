namespace EventoRemember.Locais.Domain.Validators
{
    public class ImagemValidator
    {
        private List<string> _errors = new();
        private const long _tamanhoMax = 30000;
        public ImagemValidator(string url, string nomeArquivo, long tamanhoBytes)
        {
            ValidarCaminho(url, nomeArquivo);
            ValidarTamanhoImagem(nomeArquivo, tamanhoBytes);
        }

        public IReadOnlyCollection<string> Errors => _errors.AsReadOnly();

        private void ValidarCaminho(string url, string nomeArquivo)
        {
            if (url is null || string.IsNullOrWhiteSpace(nomeArquivo))
                _errors.Add("Caminho não pode ser nulo");
        }

        private void ValidarTamanhoImagem(string nome, long tamanho)
        {
            if (tamanho > _tamanhoMax)
                _errors.Add($"O arquivo de imagem '{nome}' ultrapassa o tamanho máximo permitido de {_tamanhoMax/1000} MB");
        }
    }
}
