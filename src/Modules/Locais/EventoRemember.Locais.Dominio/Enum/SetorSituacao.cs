using System.ComponentModel;

namespace EventoRemember.Locais.Domain.Enum
{
    public enum SetorSituacao
    {
        [Description("Em análise")]
        EmAnalise = 0,

        [Description("Aprovado")]
        Aprovado,

        [Description("Capacidade invalidada")]
        CapacidadeInvalidada,

        [Description("Indisponível")]
        Indisponivel
    }
}
