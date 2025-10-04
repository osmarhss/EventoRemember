namespace EventoRemember.BuildingBlocks.Domain.VO
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public override bool Equals(object? obj)
        {
            return Equals(obj as T);
        }

        public bool Equals(T? other)
        {
            if (other is null)
                return false;

            var type = GetType();
            if (type != other.GetType())
                return false;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !(left == right);
        }
    }
}
