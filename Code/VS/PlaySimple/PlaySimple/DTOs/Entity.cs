namespace PlaySimple.DTOs
{
    public abstract class Entity<TDTO, TDomain>
    {
        public int Id { get; set; }

        public abstract TDTO Initialize(TDomain domain);
    }
}
