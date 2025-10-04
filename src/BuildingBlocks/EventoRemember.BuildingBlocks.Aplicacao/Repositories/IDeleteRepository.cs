using EventoRemember.BuildingBlocks.Domain.Entidade;

namespace EventoRemember.BuildingBlocks.Application.Repositories
{
    public interface IDeleteRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        Task DeleteAsync(Guid id);
    }
}
