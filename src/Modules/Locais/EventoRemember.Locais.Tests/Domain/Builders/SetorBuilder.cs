using Bogus;
using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.Locais.Domain.Entidades;
using EventoRemember.Locais.Domain.Enum;
using EventoRemember.Locais.Domain.VOs;

namespace EventoRemember.Locais.Tests.Domain.Entities
{
    public class SetorBuilder
    {
        private class SetorTemplate 
        {
            private SetorTemplate() { }

            public string Nome { get; set; }
            public int Capacidade { get; set; }
            public bool Ativo { get; set; }
            public SetorSituacao SetorSituacao { get; set; }
            public IEnumerable<FilaVo> Filas { get; set; } = new List<FilaVo>();

            public static SetorTemplate Gerar() 
            {
                var f = new Faker();
                
                return new SetorTemplate 
                {
                    Nome = f.Random.String2(1, 25),
                    Capacidade = f.Random.Number(1, 25),
                    Ativo = false,
                    SetorSituacao = SetorSituacao.EmAnalise,
                    Filas = new List<FilaVo>()
                };
            }
        }

        public SetorBuilder() 
        {
            _template = SetorTemplate.Gerar();
        }

        private readonly SetorTemplate _template;
        private int _quantidade = 1;

        public Result<Setor> Gerar()
            => Setor.Criar(_template.Nome, _template.Capacidade, _template.Filas);

        public List<Result<Setor>> GerarLista() 
        {
            return Enumerable.Range(1, _quantidade).Select(i => Gerar()).ToList();
        }

        public SetorBuilder ComCapacidade(int capacidade)
        {
            _template.Capacidade = capacidade;
            return this;
        }

        public SetorBuilder ComNome(string nome) 
        {
            _template.Nome = nome;
            return this;
        }

        public SetorBuilder ComFilas(IEnumerable<FilaVo> filas) 
        {
            _template.Filas = filas.ToList();
            return this;
        }

        public SetorBuilder ComQuantidadeParaLista(int qtdDeSetores) 
        {
            _quantidade = qtdDeSetores;
            return this;
        }
    }
}
