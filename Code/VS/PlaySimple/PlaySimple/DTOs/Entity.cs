namespace PlaySimple.DTOs
{
    public abstract class Entity<TDTO, TDomain>
    {
        public int? Id;

        public abstract TDTO Initialize(TDomain domain);
    }
}
