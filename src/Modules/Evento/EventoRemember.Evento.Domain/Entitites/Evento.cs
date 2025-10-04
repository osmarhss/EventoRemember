using EventoRemember.BuildingBlocks.Domain.Entidade;
using EventoRemember.Evento.Domain.VOs;

namespace EventoRemember.Evento.Domain.Entitites
{
    public class Evento : Entity, IAggregateRoot
    {
        public Endereco Endereco { get; set; }

        public void AtualizarEndereco(Endereco endereco)
        {
            Endereco = new Endereco(endereco.Logradouro, endereco.Numero, endereco.Complemento,
                endereco.Bairro, endereco.Cidade, endereco.UF, endereco.Pais, endereco.CodigoPostal);
        }
    }
}
