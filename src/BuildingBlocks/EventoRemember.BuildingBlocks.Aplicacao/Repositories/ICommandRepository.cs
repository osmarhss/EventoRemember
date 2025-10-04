using EventoRemember.BuildingBlocks.Domain.Entidade;

namespace EventoRemember.BuildingBlocks.Application.Repositories
{
    public interface ICommandRepository<TAggregateRoot> : ICreateRepository<TAggregateRoot>,
        IUpdateRepository<TAggregateRoot>, IDeleteRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
    }
}
