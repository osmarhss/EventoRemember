using EventoRemember.BuildingBlocks.Domain.Entidade;

namespace EventoRemember.BuildingBlocks.Application.Repositories
{
    public interface IUpdateRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    {
        Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregate);
    }
}
