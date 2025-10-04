using EventoRemember.BuildingBlocks.Domain.Entidade;

namespace EventoRemember.BuildingBlocks.Application.Repositories
{
    public interface IQueryRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    {
        Task<IEnumerable<TAggregateRoot>> GetAll();
        Task<IEnumerable<TAggregateRoot>> GetWithPage(int page, int pageSize);
        Task<TAggregateRoot> GetById(Guid id);
    }
}
