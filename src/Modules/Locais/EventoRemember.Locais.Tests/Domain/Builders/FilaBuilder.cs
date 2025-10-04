using Bogus;
using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.Locais.Domain.VOs;

namespace EventoRemember.Locais.Tests.Domain.Builders
{
    public class FilaBuilder
    {
        private int _quantidade = 1;

        public FilaBuilder() 
        {
            _template = FilaTemplate.Gerar();
        }
        private class FilaTemplate 
        {
            private FilaTemplate() { }

            public string Codigo { get; set; }
            public int QtdCadeiras { get; set; }

            public static FilaTemplate Gerar() 
            {
                Faker f = new Faker();

                return new FilaTemplate
                {
                    Codigo = f.Random.String2(1, 3),
                    QtdCadeiras = f.Random.Number(1, 200)
                };
            }
        }

        private readonly FilaTemplate _template;

        public Result<FilaVo> Gerar()
            => FilaVo.Criar(_template.Codigo, _template.QtdCadeiras);
        

        public List<Result<FilaVo>> GerarLista() 
            => Enumerable.Range(1, _quantidade).Select(i => Gerar()).ToList();
        

        public FilaBuilder ComCodigo(string codigo) 
        {
            _template.Codigo = codigo;
            return this;
        }

        public FilaBuilder ComQtdDeCadeiras(int quantidade) 
        {
            _template.QtdCadeiras = quantidade;
            return this;
        }

        // Método auxiliar para gerar uma quantidade de filas
        public FilaBuilder ComNumeroDeFilas(int quantidade) 
        {
            _quantidade = quantidade;
            return this;
        }
    }
}
