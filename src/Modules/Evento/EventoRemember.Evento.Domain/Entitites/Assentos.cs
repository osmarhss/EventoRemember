using EventoRemember.BuildingBlocks.Domain.VO;
using EventoRemember.Evento.Domain.Enum;

namespace EventoRemember.Evento.Domain.Entitites
{
    public class Assentos : ValueObject<Assentos>
    {
        public Assentos(string filaId, int numero, decimal? precoEspecial, StatusAssento status)
        {
            FilaId = filaId;
            Numero = numero;
            PrecoEspecial = precoEspecial;
            Status = status;
        }

        public string FilaId { get; private set; }
        public int Numero { get; private set; }
        public decimal? PrecoEspecial { get; private set; }
        public StatusAssento Status { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
