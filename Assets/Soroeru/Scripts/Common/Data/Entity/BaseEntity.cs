namespace Soroeru.Common.Data.Entity
{
    public abstract class BaseEntity<T>
    {
        public T value { get; private set; }

        public void Set(T t) => value = t;
    }
}