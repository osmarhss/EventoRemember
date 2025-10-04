using EventoRemember.BuildingBlocks.Domain.Exceptions;

namespace EventoRemember.Locais.Domain.Validators
{
    public class FilaValidator
    {
        private List<string> _errors = new();

        public FilaValidator(string codigo, int qtdCadeiras) 
        {
            ValidarNome(codigo);
            ValidarQtdDeCadeiras(qtdCadeiras);
        }

        public IReadOnlyCollection<string> Errors => _errors.AsReadOnly();

        private void ValidarNome(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
                _errors.Add("Código da fila não pode ser vazio ou nulo");

            else if (codigo.Length < 1 || codigo.Length > 3)
                _errors.Add("Código da fila precisa ter entre 1 a 3 caracteres.");
        }

        private void ValidarQtdDeCadeiras(int quantidade)
        {
            if (quantidade < 1 || quantidade > 200)
                _errors.Add("Uma fila precisa conter ao menos uma cadeira e no máximo 200.");
        }
    }
}
