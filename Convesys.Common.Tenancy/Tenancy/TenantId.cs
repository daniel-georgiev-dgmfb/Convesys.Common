namespace Convesys.Common.Tenancy.Tenancy
{
    public class TenantId<T>
    {
        public TenantId(T id)
        {
            this.Id = id;
        }
        public T Id { get; }
    }
}