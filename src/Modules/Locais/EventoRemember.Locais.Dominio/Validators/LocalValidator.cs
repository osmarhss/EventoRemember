using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.Locais.Domain.Entidades;
using EventoRemember.Locais.Domain.VOs;

namespace EventoRemember.Locais.Domain.Validators
{
    public class LocalValidator
    {
        private readonly List<string> _errors = new();

        public LocalValidator() { }

        public static Result ValidarAoCriar(string nome, int capacidade, IEnumerable<ImagemVo> imagens,
            IEnumerable<Setor> setores)
            => ValidarComo(v => 
            {
                v.ValidarNome(nome);
                v.ValidarCapacidadePermitida(capacidade);
                v.ValidarCapacidadeMaxComSetores(capacidade, setores);
                v.ValidarImagens(imagens);
                v.ValidarTotalSetores(setores);
            });

        public static Result ValidarAoAtualizarDados(string nome, int capacidade)
            => ValidarComo(v => 
            {
                v.ValidarNome(nome);
                v.ValidarCapacidadePermitida(capacidade);
            });

        public static Result ValidarAoIncluirNovoSetor(Setor setor, int capacidadeAtual, int capacidadeMax)
            => ValidarComo(v => v.ValidarAtualizacaoSetores(setor, capacidadeAtual, capacidadeMax));

        public static Result ValidarAoIncluirNovasImagens(IEnumerable<ImagemVo> imagensAtuais, IEnumerable<ImagemVo> novasImagens)
            => ValidarComo(v =>
            {
                v.ValidarNovasImagens(imagensAtuais, novasImagens);
            });

        public static Result ValidarComo(Action<LocalValidator> regras)
        {
            var validator = new LocalValidator();
            regras(validator);

            if (validator._errors.Count > 0)
                return Result.Failure(validator._errors);

            return Result.Success();
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                _errors.Add("O nome do local não pode ser nulo ou vazio");

            if (nome.Length < 4 || nome.Length > 25)
                _errors.Add("O nome do local deve possuir entre 4 a 20 caracteres");
        }

        private void ValidarCapacidadePermitida(int capacidade)
        {
            if (capacidade < 20 || capacidade > 500000)
                _errors.Add("O local precisa ter capacidade entre 20 e 500000 pessoas");
        }

        private void ValidarImagens(IEnumerable<ImagemVo> imagens) 
        {
            ValidarQuantidadeDeImagens(imagens.Count());
        }

        private void ValidarNovasImagens(IEnumerable<ImagemVo> imagensAtuais, IEnumerable<ImagemVo> novasImagens) 
        {
            ValidarQuantidadeDeImagens(imagensAtuais.Count() + novasImagens.Count());

            int contador = 0;

            foreach (var imagem in novasImagens)
            {
                if (imagensAtuais.Contains(imagem))
                    contador++;
            }

            if (contador == 1)
                _errors.Add("Há uma imagem com o mesmo nome em nossos registros");

            else if (contador > 1)
                _errors.Add($"Há {contador} imagens com o mesmo nome em nossos registros");
        }

        private void ValidarQuantidadeDeImagens(int quantidade) 
        {
            if (quantidade > 10)
                _errors.Add("O máximo de fotos permitido é de 10.");
        }

        private void ValidarTotalSetores(IEnumerable<Setor> setores) 
        {
            if (setores.Count() > 25)
                _errors.Add("O máximo de setores permitidos é de 25.");
        }

        private void ValidarCapacidadeMaxComSetores(int capacidade, IEnumerable<Setor> setores)
        {
            var capacidadeTotalSetores = setores.Sum(s => s.Capacidade);

            if (capacidadeTotalSetores > capacidade)
                _errors.Add("A soma da capacidade dos setores é maior que a capacidade máxima do local");
        }

        private void ValidarAtualizacaoSetores(Setor setor, int capacidadeAtual, int capacidadeMax)
        {  
            if (setor.Capacidade + capacidadeAtual > capacidadeMax)
                _errors.Add("A soma da capacidade dos setores é maior que a capacidade máxima do local");
        }
    }
}
