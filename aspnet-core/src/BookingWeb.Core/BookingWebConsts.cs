using BookingWeb.Debugging;

namespace BookingWeb
{
    public class BookingWebConsts
    {
        public const string LocalizationSourceName = "BookingWeb";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "fa8a47c90e8340c98a44625dadc7dc87";
    }
}
