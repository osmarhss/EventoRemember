using EventoRemember.BuildingBlocks.Domain.Entidade;

namespace EventoRemember.BuildingBlocks.Application.Repositories
{
    public interface ICreateRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        Task<TAggregateRoot> CreateAsync(TAggregateRoot aggregate);
    }
}
