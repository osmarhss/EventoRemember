using EventoRemember.BuildingBlocks.Domain.Entidade;
using EventoRemember.BuildingBlocks.Domain.Exceptions;
using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.Locais.Domain.Enum;
using EventoRemember.Locais.Domain.Validators;
using EventoRemember.Locais.Domain.VOs;

namespace EventoRemember.Locais.Domain.Entidades
{
    public class Setor : Entity
    {
        private readonly List<FilaVo> _filas = new();

        protected Setor() { } //EF
        private Setor(string nome, int capacidade, IEnumerable<FilaVo> filas) : base()
        {
            Nome = nome;
            Capacidade = capacidade;
            Ativo = false;
            Situacao = SetorSituacao.EmAnalise;
            _filas = filas.ToList() ?? new();
        }

        public string Nome { get; private set; }
        public int Capacidade { get; private set; }
        public bool Ativo { get; private set; } = false;
        public SetorSituacao Situacao { get; private set; } = SetorSituacao.EmAnalise;
        public IReadOnlyCollection<FilaVo> Filas => _filas.AsReadOnly();

        public static Result<Setor> Criar(string nome, int capacidade, IEnumerable<FilaVo> filas)
        {
            var validatorRes = SetorValidator.ValidarAoCriar(nome, capacidade);

            if (!validatorRes.IsSuccess)
                return Result<Setor>.Failure(validatorRes.Errors);

            var setor = new Setor(nome, capacidade, filas);

            return Result<Setor>.Success(setor);
        }

        public Result AtualizarDados(string nome, int capacidade)
        {
            var validatorRes = SetorValidator.ValidarAoAtualizar(nome, capacidade, _filas.Any());

            if(!validatorRes.IsSuccess)
                return Result.Failure(validatorRes.Errors);

            Nome = nome;
            
            if(!_filas.Any())
                Capacidade = capacidade;

            return Result.Success();
        }

        public Result GerirFilas(IEnumerable<FilaVo> filasAlteradas)
        {
            var capacidadeNova = filasAlteradas.Sum(f => f.QtdCadeiras);

            var validatorRes = SetorValidator.ValidarAoGerirFilas(capacidadeNova);

            if(!validatorRes.IsSuccess)
                return Result.Failure(validatorRes.Errors);

            Capacidade = capacidadeNova;

            _filas.Clear();
            _filas.AddRange(filasAlteradas);

            return Result.Success();
        }

        public void AtualizarSituacaoPeloManager(SetorSituacao situacao)
        {
            Ativo = situacao switch
            {
                SetorSituacao.Aprovado => Ativo = true,
                SetorSituacao.EmAnalise => Ativo = false,
                SetorSituacao.Indisponivel => throw new DomainException("Somente o usuário pode definir como indisponível"),
                _ => throw new DomainException("Entrada inválida"),
            };

            Situacao = situacao;
        }

        public void AtualizarSituacaoPeloUsuario()
        {
            Situacao = SetorSituacao.Indisponivel;
            Ativo = false;
        }
    }
}