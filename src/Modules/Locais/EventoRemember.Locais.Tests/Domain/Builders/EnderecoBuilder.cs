using Bogus;
using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.Locais.Domain.Validators;
using EventoRemember.Locais.Domain.VOs;

namespace EventoRemember.Locais.Tests.Domain.Builders
{
    public class EnderecoBuilder
    {
        private class EnderecoTemplate 
        {
            private EnderecoTemplate() { }

            public string Logradouro { get; set; }
            public string Numero { get; set; }
            public string? Complemento { get; set; } = null;
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string UF { get; set; }
            public string Pais { get; set; }
            public string CodigoPostal { get; set; }

            public static EnderecoTemplate Gerar() 
            {
                var f = new Faker("pt_BR");
                return new EnderecoTemplate
                {
                    Logradouro = f.Random.String2(5, 50),
                    Numero = f.Random.Number(1, 6).ToString(),
                    Complemento = null,
                    Bairro = f.Random.String2(3, 25),
                    Cidade = f.Random.String2(3, 30),
                    UF = f.Random.ArrayElement(_estadosArray),
                    Pais = "Brasil",
                    CodigoPostal = f.Random.Number(10000000, 99999999).ToString()
                };
            } 
        }

        private readonly EnderecoTemplate _template;

        public EnderecoBuilder() 
        {
            _template = EnderecoTemplate.Gerar();
        }

        private static readonly string[] _estadosArray = EnderecoValidator.EstadosBrasileiros.ToArray();

        public Result<EnderecoVo> Gerar()
            => EnderecoVo.Criar(
                _template.Logradouro,
                _template.Numero,
                _template.Complemento,
                _template.Bairro,
                _template.Cidade,
                _template.UF,
                _template.Pais,
                _template.CodigoPostal
            );

        public EnderecoBuilder ComLogradouro(string logradouro) 
        {
            _template.Logradouro = logradouro;
            return this;
        }

        public EnderecoBuilder ComNumero(string numero)
        {
            _template.Numero = numero;
            return this;
        }

        public EnderecoBuilder ComComplemento(string complemento)
        {
            _template.Complemento = complemento;
            return this;
        }

        public EnderecoBuilder ComBairro(string bairro)
        {
            _template.Bairro = bairro;
            return this;
        }

        public EnderecoBuilder ComCidade(string cidade)
        {
            _template.Cidade = cidade;
            return this;
        }

        public EnderecoBuilder ComUf(string uf) 
        {
            _template.UF = uf;
            return this;
        }

        public EnderecoBuilder ComPais(string pais) 
        {
            _template.Pais = pais;
            return this;
        }

        public EnderecoBuilder ComCodigoPostal(string cep) 
        {
            _template.CodigoPostal = cep;
            return this;
        }
    }
}
