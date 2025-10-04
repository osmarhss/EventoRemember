using System.Security.Principal;

namespace EventoRemember.BuildingBlocks.Domain.Entidade
{
    public abstract class Entity : IEntity, IEquatable<Entity>
    {
        public Guid Id { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public bool Equals(Entity? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;

            return Id == other.Id;
        }

        public override bool Equals(object? obj) => Equals(obj as Entity);
        public override int GetHashCode() => Id.GetHashCode();
    }
}
