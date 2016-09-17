namespace PlaySimple.DTOs
{
    public abstract class Entity<TDTO, TDomain>
    {
        public int? Id;

        public abstract TDTO Initialize(TDomain domain);

        public override bool Equals(System.Object obj)
        {
            Entity<TDTO, TDomain> p = obj as Entity<TDTO, TDomain>;
            if ((object)p == null)
            {
                return false;
            }

            return base.Equals(p);
        }

        public bool Equals(Entity<TDTO, TDomain> obj)
        {

            return this.Id == obj.Id;
        }
    }
}
