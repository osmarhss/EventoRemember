using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.Locais.Domain.VOs;

namespace EventoRemember.Locais.Domain.Validators
{
    public class SetorValidator
    {
        private List<string> _errors = new();

        public IReadOnlyCollection<string> Errors => _errors.AsReadOnly();

        public static Result ValidarAoCriar(string nome, int capacidade)
            => ValidarComo(v => 
            {
                v.ValidarNome(nome);
                v.ValidarCapacidade(capacidade);
            });

        public static Result ValidarAoAtualizar(string nome, int capacidade, bool temFilas)
            => ValidarComo(v => 
            {
                v.ValidarNome(nome);
                if (!temFilas)
                    v.ValidarCapacidade(capacidade);
            });

        public static Result ValidarAoGerirFilas(int novaCapacidade)
            => ValidarComo(v => v.ValidarCapacidade(novaCapacidade));
        

        private static Result ValidarComo(Action<SetorValidator> regras) 
        {
            var validator = new SetorValidator();
            regras(validator);

            if (validator.Errors.Any())
                return Result.Failure(validator._errors);

            return Result.Success();
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
               _errors.Add("O nome do setor não pode ser vazio ou nulo");

            else if (nome.Length < 1 || nome.Length > 25)
                _errors.Add("O nome do setor precisa ter entre 1 a 25 caracteres");
        }

        private void ValidarCapacidade(int capacidade)
        {
            if (capacidade < 1 || capacidade > 20000)
                _errors.Add("A capacidade do setor não pode ser menor que 1 e maior que 20.000");
        }
    }
}
