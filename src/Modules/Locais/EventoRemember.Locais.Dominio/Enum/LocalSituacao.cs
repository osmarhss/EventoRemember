using System.ComponentModel;

namespace EventoRemember.Locais.Domain.Enum
{
    public enum LocalSituacao
    {
        [Description("Em análise")]
        EmAnalise = 0,

        [Description("Aprovado")]
        Aprovado,

        [Description("Indisponível")]
        Indisponivel,

        [Description("Inativo")]
        Inativo,

        [Description("Inválido")]
        Inválido
    }
}
