namespace EventoRemember.Locais.Domain.Validators
{
    public class EnderecoValidator
    {
        private List<string> _errors = new();
        private static readonly IReadOnlyCollection<string> _estadosBrasileiros = new HashSet<string>
        {
            "Acre", "Alagoas", "Amazonas", "Bahia", "Ceará", "Distrito Federal", "Espírito Santo",
            "Goiás", "Maranhão", "Mato Grosso", "Mato Grosso do Sul", "Minas Gerais",
            "Pará", "Paraíba", "Paraná", "Pernambuco", "Piauí", "Rio de Janeiro",
            "Rio Grande do Norte", "Rio Grande do Sul", "Rondônia", "Roraima",
            "Santa Catarina", "São Paulo", "Sergipe", "Tocantins"
        };

        public EnderecoValidator(string logradouro, string numero, string? complemento, string bairro,
            string cidade, string uf, string pais, string codigoPostal)
        {
            ValidarLogradouro(logradouro);
            ValidarNumero(numero);
            ValidarComplemento(complemento);
            ValidarBairro(bairro);
            ValidarCidade(cidade);
            ValidarUF(uf);
            ValidarPais(pais);
            ValidarCep(codigoPostal);
        }

        public IReadOnlyCollection<string> Errors => _errors.AsReadOnly();
        public static IReadOnlyCollection<string> EstadosBrasileiros => _estadosBrasileiros;

        private void ValidarLogradouro(string nome) => ValidarTamanho(nome, "Logradouro", 5, 50);

        private void ValidarNumero(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                _errors.Add("Número do local não pode ser nulo ou vazio");

            else if (!numero.All(char.IsDigit))
                _errors.Add("Número do local deve conter apenas números");

            else if (numero.Length > 6)
                _errors.Add("Número do local é muito grande");
        }

        private void ValidarComplemento(string? complemento)
        {
            if (complemento != null && complemento.Length > 20)
                _errors.Add("Complemento do local é muito grande");
        }

        private void ValidarBairro(string bairro) => ValidarTamanho(bairro, "Bairro", 3, 25);

        private void ValidarCidade(string cidade) => ValidarTamanho(cidade, "Cidade", 3, 30);

        private void ValidarUF(string uf)
        {
            if (string.IsNullOrWhiteSpace(uf))
                _errors.Add("Estado não pode ser nulo ou vazio");

            else if (!_estadosBrasileiros.Contains(uf))
                _errors.Add("Estado inválido");
        }

        private void ValidarPais(string pais)
        {
            if (string.IsNullOrWhiteSpace(pais))
                _errors.Add("País não pode ser nulo ou vazio");

            //Um único país válido inicialmente
            else if (!pais.Equals("Brasil"))
                _errors.Add("Local precisa estar no Brasil");
        }

        private void ValidarCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                _errors.Add("CEP não pode ser nulo ou vazio");

            else if (cep.Length != 8 || !cep.All(char.IsDigit))
                _errors.Add("CEP inválido");
        }

        private void ValidarTamanho(string value, string nomeCampo, int minimo, int maximo) 
        {
            if (string.IsNullOrWhiteSpace(value))
                _errors.Add($"{nomeCampo} não pode ser nulo ou vazio");
            
            else if (value.Length < minimo || value.Length > maximo)
                _errors.Add($"{nomeCampo} deve possuir entre {minimo} e {maximo} de caracteres");
               
        }
    }
}
