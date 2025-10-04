using EventoRemember.BuildingBlocks.Domain.Entidade;
using EventoRemember.BuildingBlocks.Domain.Exceptions;
using EventoRemember.Locais.Domain.Entidades;
using EventoRemember.Locais.Domain.VOs;
using EventoRemember.Locais.Dominio.Enum;

namespace EventoRemember.Locais.Dominio.Entidades
{
    public class Local : Entity, IAggregateRoot
    {
        private readonly List<ImagemVo> _imagens = new();
        private readonly List<Setor> _setores = new();

        protected Local() { } //EF
        private Local(string nome, int capacidadeTotal, TipoLocal tipo, EnderecoVo endereco, 
            IEnumerable<ImagemVo> imagens, IEnumerable<Setor> setores) : base()
        {
            Nome = nome;
            CapacaidadeTotal = capacidadeTotal;
            Tipo = tipo;
            Endereco = endereco;
            _imagens = imagens.ToList() ?? new();
            _setores = setores.ToList() ?? new();
        }

        public string Nome { get; private set; }
        public int CapacaidadeTotal { get; private set; }
        public bool Ativo { get; private set; } = false;
        public TipoLocal Tipo { get; private set; }
        public EnderecoVo Endereco { get; private set; }
        public IReadOnlyCollection<Setor> Setores => _setores.AsReadOnly();
        public IReadOnlyCollection<ImagemVo> Imagens => _imagens.AsReadOnly();

        public static Local Criar(string nome, int capacidadeTotal, TipoLocal tipo, EnderecoVo endereco, 
            IEnumerable<ImagemVo> imagens, IEnumerable<Setor> setores) 
        {
            ValidarNome(nome);

            if (setores.Any())
                ValidarCapacidadeMax(capacidadeTotal, setores);

            return new Local(nome, capacidadeTotal, tipo, endereco, imagens, setores);
        }

        public void AdicionarImagens(IEnumerable<ImagemVo> listaImagensAdd) 
        {
            if (_imagens.Count + listaImagensAdd.Count() > 10)
                throw new DomainException("Capacidade máxima de fotos atingida.");

            foreach (var imagem in listaImagensAdd)
            {
                if (_imagens.Contains(imagem))
                    throw new DomainException("Imagens duplicadas não são permitidas");

                _imagens.Add(imagem);
            }
        }

        public void AtualizarDados(string nome, TipoLocal tipo)
        {
            Nome = nome;
            Tipo = tipo;
        }

        public void IncluirSetores(IEnumerable<Setor> setores) 
        {
            ValidarCapacidadeMax(setores);

            _setores.AddRange(setores);
        }

        public void SubstiuirSetores(IEnumerable<Setor> setores)
        {
            ValidarCapacidadeMax(setores);

            _setores.Clear();
            _setores.AddRange(setores);
        }

        public void ExcluirSetores(IEnumerable<Setor> setores) 
        {
            foreach (var setor in setores) 
            {
                if (setores.Contains(setor)) 
                    _setores.Remove(setor);
            }
        }

        public void AtualizarEndereco(EnderecoVo novo) 
        {
            Endereco = novo;
        }

        public void DefinirCapacidadeMax(int quantidade) 
        {
            CapacaidadeTotal = quantidade;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new DomainException("O nome do local não pode ser nulo ou vazio");

            if (nome.Length < 4 || nome.Length > 25)
                throw new DomainException("O nome do local deve possuir entre 4 a 20 caracteres");
        }

        private static void ValidarCapacidadeMax(int capacidade, IEnumerable<Setor> setores) 
        {
            var capacidadeTotalSetores = setores.Sum(s => s.Capacidade);

            if (capacidadeTotalSetores > capacidade)
                throw new DomainException("A soma da capacidade dos setores é maior que a capacidade máxima do local");          
        }
        private void ValidarCapacidadeMax(IEnumerable<Setor> setores) 
        {
            var capacidadeTotalNova = 0;
            
            _setores.ForEach(c => capacidadeTotalNova += c.Capacidade);

            foreach (var setor in setores)
            {
                capacidadeTotalNova += setor.Capacidade;
            }

            if (capacidadeTotalNova > CapacaidadeTotal)
                throw new DomainException("A soma da capacidade dos setores é maior que a capacidade máxima do local");          
        }
    }
}
