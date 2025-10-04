using EventoRemember.BuildingBlocks.Domain.Exceptions;
using EventoRemember.BuildingBlocks.Domain.Validators;
using EventoRemember.Locais.Domain.VOs;

namespace EventoRemember.Locais.Domain.Services
{
    public class GeradorDeFilas
    {
        private const string colecaoAlfabetica = "ABCDEFGHJKLMNPQRSTUVWXYZ";

        public static List<Result<FilaVo>> GerarFilasAlfabetoApenas(Dictionary<int, int> setupFila)
        {
            if (setupFila.Keys.Count > colecaoAlfabetica.Length)
                throw new DomainException("O número de fileiras ultrapassa o valor de letras para esse padrão");

            List<Result<FilaVo>> filasRes = new();

            for (int i = 1; i <= setupFila.Keys.Count; i++)
            {
                var letra = colecaoAlfabetica[i - 1];

                if (setupFila.TryGetValue(letra, out int qtdDeCadeiras))                
                    filasRes.Add(FilaVo.Criar(letra.ToString(), qtdDeCadeiras));                     
            }

            return filasRes;
        }

        public static List<Result<FilaVo>> GerarFilasNumericas(Dictionary<int, int> setupFila) 
        {
            List<Result<FilaVo>> filasRes = new();

            for (int i = 1; i <= setupFila.Keys.Count; i++) 
            {
                if (setupFila.TryGetValue(i, out int qtdDeCadeiras)) 
                    filasRes.Add(FilaVo.Criar($"0{setupFila[i]}", qtdDeCadeiras));             
            }

            return filasRes;
        }
    }
}
