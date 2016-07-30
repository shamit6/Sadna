namespace PlaySimple
{
    public static class Consts
    {
        public const string DB_PATH = "~/App_Data/db.sqlite";

        public static class Roles
        {
            public const string Customer = "Customer";
            public const string Employee = "Employee";
            public const string Admin = "Admin";
        }

        public static class Paging
        {
            public const int PageSize = 10;
        }

        public static class Decodes
        {
            public enum OrderStatus
            {
                Sent,
                Accepted,
                Rejected
            }

            public enum ComplaintType
            {

            }

            public enum FieldSize
            {

            }

            public enum FieldType
            {

            }

            public enum InvitationStatus
            {

            }

            public enum RegionDecode
            {

            }
        }
    }
}