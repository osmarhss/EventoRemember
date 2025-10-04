using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.BuildingBlocks.Domain.VO;
using EventoRemember.Locais.Domain.Validators;

namespace EventoRemember.Locais.Domain.VOs
{
    public class ImagemVo : ValueObject<ImagemVo>
    {
        private ImagemVo(string url, string nomeArquivo, long tamanhoBytes)
        {
            Url = url;
            NomeArquivo = nomeArquivo;
            TamanhoBytes = tamanhoBytes;
        }

        public string Url { get; }
        public string NomeArquivo { get; }
        public long TamanhoBytes { get; }

        public static Result<ImagemVo> Criar(string url, string nomeArquivo, long tamanhoBytes) 
        {
            var validator = new ImagemValidator(url, nomeArquivo, tamanhoBytes);

            if (validator.Errors.Any())
                return Result<ImagemVo>.Failure(validator.Errors);

            var vo = new ImagemVo(url, nomeArquivo, tamanhoBytes);

            return Result<ImagemVo>.Success(vo);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
            yield return NomeArquivo;
        }
    }
}
