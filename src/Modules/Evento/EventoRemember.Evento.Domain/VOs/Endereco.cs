namespace EventoRemember.Evento.Domain.VOs
{
    public class Endereco
    {
        public Endereco(string logradouro, string numero, string? complemento, string bairro, string cidade, string uF, string pais, string codigoPostal)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            UF = uF;
            Pais = pais;
            CodigoPostal = codigoPostal;
        }

        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string? Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        public string Pais { get; private set; }
        public string CodigoPostal { get; private set; }
    }
}
