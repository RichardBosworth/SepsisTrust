namespace SepsisTrust.Model.User
{
    /// <summary>
    /// Provides a static memory-based store for storage of app user data.
    /// </summary>
    public static class StaticUserDataStore
    {
        public const string UserFileName = "userdetails.dat";

        public static AppUserData UserData { get; set; }

        static StaticUserDataStore( )
        {
        }
    }
}