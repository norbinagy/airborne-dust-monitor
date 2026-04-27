using System.Security.Cryptography;
using System.Text;

namespace AirborneDustMonitor.Infrastructure
{
    // Ez a segédosztály a Windows Data Protection API-t használja a jelszavak biztonságos tárolásához. A Protect metódus titkosítja a sima szöveget, míg a TryUnprotect megpróbálja visszafejteni a titkosított szöveget és visszaadja az eredményt.
    public static class DataProtectionHelper
    {
        public static string Protect(string plainText)
        {
            var bytes = Encoding.UTF8.GetBytes(plainText);

            var protectedBytes = ProtectedData.Protect(
                bytes,
                optionalEntropy: null,
                scope: DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(protectedBytes);
        }

        public static bool TryUnprotect(string protectedText, out string? plainText)
        {
            try
            {
                var bytes = Convert.FromBase64String(protectedText);
                var unprotectedBytes = ProtectedData.Unprotect(bytes, optionalEntropy: null, scope: DataProtectionScope.CurrentUser);
                plainText = Encoding.UTF8.GetString(unprotectedBytes);
                return true;
            }
            catch (Exception)
            {
                plainText = null;
                return false;
            }
        }
    }
}
