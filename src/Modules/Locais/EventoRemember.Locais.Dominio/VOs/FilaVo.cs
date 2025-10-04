using EventoRemember.BuildingBlocks.Domain.Exceptions;
using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.BuildingBlocks.Domain.VO;
using EventoRemember.Locais.Domain.Validators;

namespace EventoRemember.Locais.Domain.VOs
{
    public class FilaVo : ValueObject<FilaVo>
    {
        private FilaVo(string codigo, int qtdCadeiras)
        {
            Codigo = codigo;
            QtdCadeiras = qtdCadeiras;
        }

        public string Codigo { get; }
        public int QtdCadeiras { get; }

        public static Result<FilaVo> Criar(string codigo, int qtdCadeiras)
        {
            var validator = new FilaValidator(codigo, qtdCadeiras);

            if(validator.Errors.Any()) 
                return Result<FilaVo>.Failure(validator.Errors);

            var vo = new FilaVo(codigo, qtdCadeiras);

            return Result<FilaVo>.Success(vo);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Codigo;
        }
    }
}