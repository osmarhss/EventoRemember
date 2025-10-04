using Bogus;
using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.Locais.Domain.VOs;

namespace EventoRemember.Locais.Tests.Domain.Builders
{
    public class ImagemBuilder
    {
        private int _quantidade = 1;

        public ImagemBuilder() 
        {
            _template = ImagemTemplate.Gerar();
        }

        private class ImagemTemplate 
        {
            private ImagemTemplate() { }

            public string Url { get; set; }
            public string NomeArquivo { get; set; }
            public long TamanhoBytes { get; set; }

            public static ImagemTemplate Gerar() 
            {
                var f = new Faker("pt_BR");

                return new ImagemTemplate
                {
                    Url = f.Random.String2(1, 100),
                    NomeArquivo = f.Random.String2(1, 50),
                    TamanhoBytes = f.Random.Long(1, 30000)
                };
            }
        }

        private readonly ImagemTemplate _template;

        public Result<ImagemVo> Gerar() 
            => ImagemVo.Criar(
                _template.Url,
                _template.NomeArquivo,
                _template.TamanhoBytes
                );

        public List<Result<ImagemVo>> GerarLista() 
            => Enumerable.Range(1, _quantidade).Select(i => Gerar()).ToList();

        public ImagemBuilder ComUrl(string url) 
        {
            _template.Url = url;
            return this;
        }

        public ImagemBuilder ComNomeArquivo(string nomeArquivo) 
        {
            _template.NomeArquivo = nomeArquivo;
            return this;
        }

        public ImagemBuilder ComTamanhoBytes(long tamanhoBytes) 
        {
            _template.TamanhoBytes = tamanhoBytes;
            return this;
        }

        public ImagemBuilder ComQuantidade(int quantidade) 
        {
            _quantidade = quantidade;
            return this;
        }
    }
}
