using Bogus;
using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.Locais.Domain.Entidades;
using EventoRemember.Locais.Domain.Enum;
using EventoRemember.Locais.Domain.VOs;
using EventoRemember.Locais.Dominio.Entidades;
using EventoRemember.Locais.Dominio.Enum;
using EventoRemember.Locais.Tests.Domain.Entities;

namespace EventoRemember.Locais.Tests.Domain.Builders
{
    public class LocalBuilder
    {
        private class LocalTemplate
        {
            public string Nome { get; set; }
            public string Descricao { get; set; }
            public int CapacaidadeTotal { get; set; }
            public bool Ativo { get; set; } = false;
            public bool PodePersonalizarSetores { get; set; }
            public double? MetrosQuadUtilizaveis { get; set; }
            public string? MotivoInvalido { get; set; }
            public TipoLocal Tipo { get; set; }
            public LocalSituacao Situacao { get; set; }
            public EnderecoVo Endereco { get; set; }
            public IEnumerable<Setor> Setores { get; set; }
            public IEnumerable<ImagemVo> Imagens { get; set; }

            public static LocalTemplate Gerar()
            {
                var f = new Faker("pt_BR");
                return new LocalTemplate
                {
                    Nome = f.Random.String2(4, 20),
                    Descricao = f.Random.String2(15, 500),
                    CapacaidadeTotal = f.Random.Int(20, 500000),
                    Ativo = false,
                    PodePersonalizarSetores = f.Random.Bool(),
                    Tipo = f.PickRandom<TipoLocal>(),
                    Situacao = f.PickRandom<LocalSituacao>(),
                    Endereco = new EnderecoBuilder().Gerar().Value!,
                    Setores = new SetorBuilder().ComQuantidadeParaLista(new Random().Next(0, 25)).GerarLista().Select(s => s.Value!),
                    Imagens = new ImagemBuilder().ComQuantidade(new Random().Next(0, 10)).GerarLista().Select(i => i.Value!),
                };
            }
        }

        public LocalBuilder()
        {
            _template = LocalTemplate.Gerar();
        }

        private readonly LocalTemplate _template;

        public Result<Local> Gerar()
            => Local.Criar(_template.Nome, _template.Descricao, _template.CapacaidadeTotal, _template.PodePersonalizarSetores,
                _template.MetrosQuadUtilizaveis, _template.Tipo, _template.Endereco, _template.Imagens, _template.Setores);

        public LocalBuilder ComNome(string nome)
        {
            _template.Nome = nome;
            return this;
        }

        public LocalBuilder ComDescricao(string descricao)
        {
            _template.Descricao = descricao;
            return this;
        }

        public LocalBuilder ComCapacidadeTotal(int capacidadeTotal)
        {
            _template.CapacaidadeTotal = capacidadeTotal;
            return this;
        }

        public LocalBuilder ComPoderPersonalizarSetores(bool podePersonalizar) 
        {
            _template.PodePersonalizarSetores = podePersonalizar;
            return this;
        }

        public LocalBuilder ComMetrosQuadradosUtilizaveis(int metrosQuadrados) 
        {
            _template.MetrosQuadUtilizaveis = metrosQuadrados;
            return this;
        }

        public LocalBuilder ComAtivo(bool ativo) 
        {
            _template.Ativo = ativo;
            return this;
        }

        public LocalBuilder ComTipo(TipoLocal tipo) 
        {
            _template.Tipo = tipo;
            return this;
        }

        public LocalBuilder ComSituacao(LocalSituacao situacao) 
        {
            _template.Situacao = situacao;
            return this;
        }

        public LocalBuilder ComEndereco(EnderecoVo endereco) 
        {
            _template.Endereco = endereco;
            return this;
        }

        public LocalBuilder ComSetores(IEnumerable<Setor> setores)
        {
            _template.Setores = setores;
            return this;
        }

        public LocalBuilder ComImagens(IEnumerable<ImagemVo> imagems) 
        {
            _template.Imagens = imagems;
            return this;
        }
    }
}
