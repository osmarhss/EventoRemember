using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.BuildingBlocks.Domain.VO;

namespace EventoRemember.Evento.Domain.VOs
{
    public class PeriodoVo : ValueObject<PeriodoVo>
    {
        public PeriodoVo(DateTime inicio, DateTime fim)
        {
            Inicio = inicio;
            Fim = fim;
        }

        public DateTime Inicio { get; private set; }
        public DateTime Fim { get; private set; }
        

        public static Result<PeriodoVo> Criar(DateTime inicio, DateTime fim)
        {
            if (fim < inicio)
                return Result<PeriodoVo>.Failure(new List<string>() { "Data final não pode ser anterior à data inicial" });

            var vo = new PeriodoVo(inicio, fim);

            return Result<PeriodoVo>.Success(vo);
        }

        public TimeSpan TempoRestanteParaIniciar()
            => Inicio.Subtract(DateTime.Now);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Inicio;
            yield return Fim;
        }
    }
}
