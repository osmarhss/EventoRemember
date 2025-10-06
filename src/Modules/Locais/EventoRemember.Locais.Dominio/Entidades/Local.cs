using EventoRemember.BuildingBlocks.Domain.Entidade;
using EventoRemember.BuildingBlocks.Domain.Exceptions;
using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.Locais.Domain.Entidades;
using EventoRemember.Locais.Domain.Validators;
using EventoRemember.Locais.Domain.VOs;
using EventoRemember.Locais.Dominio.Enum;

namespace EventoRemember.Locais.Dominio.Entidades
{
    public class Local : Entity, IAggregateRoot
    {
        private readonly List<ImagemVo> _imagens = new();
        private readonly List<Setor> _setores = new();

        protected Local() { } //EF
        private Local(string nome, string descricao, int capacidadeTotal, bool setoresPersonalizaveis, double? metrosQuadUtilizaveis, 
            TipoLocal tipo, EnderecoVo endereco, IEnumerable<ImagemVo> imagens, IEnumerable<Setor> setores) : base()
        {
            Nome = nome;
            CapacaidadeTotal = capacidadeTotal;
            Ativo = false;
            PodePersonalizarSetores = setoresPersonalizaveis;
            MetrosQuadUtilizaveis = metrosQuadUtilizaveis;
            Tipo = tipo;
            Endereco = endereco;
            _imagens = imagens.ToList() ?? new();
            _setores = setores.ToList() ?? new();
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int CapacaidadeTotal { get; private set; }
        public bool Ativo { get; private set; } = false;
        public bool PodePersonalizarSetores { get; private set; }
        public double? MetrosQuadUtilizaveis { get; private set; }
        public string? MotivoInvalido { get; private set; }
        public TipoLocal Tipo { get; private set; }
        public EnderecoVo Endereco { get; private set; }
        public IReadOnlyCollection<Setor> Setores => _setores.AsReadOnly();
        public IReadOnlyCollection<ImagemVo> Imagens => _imagens.AsReadOnly();

        public static Result<Local> Criar(string nome, string descricao, int capacidadeTotal, bool setoresPersonalizaveis, double metrosQuadrados, 
            TipoLocal tipo, EnderecoVo endereco, IEnumerable<ImagemVo> imagens, IEnumerable<Setor> setores) 
        {
            var validator = LocalValidator.ValidarAoCriar(nome, capacidadeTotal, imagens, setores);

            if (validator.Errors.Count > 0)
                return Result<Local>.Failure(validator.Errors);

            var local = new Local(nome, descricao, capacidadeTotal, setoresPersonalizaveis, metrosQuadrados, tipo, endereco, imagens, setores);

            return Result<Local>.Success(local);
        }

        public Result AtualizarDados(string nome, int capacidadeMax, TipoLocal tipo)
        {
            var validator = LocalValidator.ValidarAoAtualizarDados(nome, capacidadeMax);

            if (validator.Errors.Count > 0)
                return Result.Failure(validator.Errors);

            Nome = nome;
            CapacaidadeTotal = capacidadeMax;

            if ((int)tipo > 10)
                throw new DomainException("Opção de tipo de local inexistante");
            
            Tipo = tipo;

            return Result.Success();
        }

        public Result AdicionarNovasImagens(IEnumerable<ImagemVo> novasImagens) 
        {
            var validator = LocalValidator.ValidarAoIncluirNovasImagens(_imagens, novasImagens);

            if (validator.Errors.Count > 0)
                return Result.Failure(validator.Errors);

            _imagens.AddRange(novasImagens);
            return Result.Success();
        }

        public Result IncluirSetor(Setor setor)
        {
            var capacidadeAtual = _setores.Sum(s => s.Capacidade);
            var validator = LocalValidator.ValidarAoIncluirNovoSetor(setor, capacidadeAtual, CapacaidadeTotal);

            if (validator.Errors.Count > 0)
                return Result.Failure(validator.Errors);

            _setores.Add(setor);

            return Result.Success();
        }
        
        public void RemoverSetor(Setor setor) 
            => _setores.Remove(setor);

        public void AtualizarEndereco(EnderecoVo novo) 
            => Endereco = novo;
        
    }
}
