using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.Locais.Domain.Validators;

namespace EventoRemember.Locais.Domain.VOs
{
    public class EnderecoVo
    {
        protected EnderecoVo() { } //EF
        private EnderecoVo(string logradouro, string numero, string? complemento, string bairro, 
            string cidade, string uf, string pais, string codigoPostal)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
            Pais = pais;
            CodigoPostal = codigoPostal;
        }

        public string Logradouro { get; }
        public string Numero { get; }
        public string? Complemento { get; }
        public string Bairro { get; }
        public string Cidade { get; }
        public string UF { get; }
        public string Pais { get; }
        public string CodigoPostal { get; }

        public static Result<EnderecoVo> Criar(string logradouro, string numero, string? complemento, string bairro,
            string cidade, string uf, string pais, string codigoPostal)
        {
            var validator = new EnderecoValidator(logradouro, numero, complemento, bairro, cidade, uf, pais, codigoPostal);
            
            if(validator.Errors.Any())
                return Result<EnderecoVo>.Failure(validator.Errors);

            var vo = new EnderecoVo(logradouro, numero, complemento, bairro, cidade, uf, pais, codigoPostal);
            return Result<EnderecoVo>.Success(vo);
        }
    }
}
