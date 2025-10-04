using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoRemember.Locais.Domain.Validators
{
    public class PeriodoValidator
    {
        private List<string> _errors = new();

        public PeriodoValidator(DateTime inicio, DateTime fim)
        {
            ValidarDataEvento(inicio, fim);
        }

        private void ValidarDataEvento(DateTime inicio, DateTime fim) 
        {
            if(inicio > fim)
                _errors.Add("O fim do evento não pode ser antes do início");

            if (DateTime.Now < DateTime.Now.AddYears(5))
                _errors.Add("Só é permitido o cadastro de eventos com no máximo 5 anos de antecedência");

            if (fim > inicio.AddDays(8))
                _errors.Add("Só é permitido eventos com período total máximo de 8 dias");
        }
    }
}
