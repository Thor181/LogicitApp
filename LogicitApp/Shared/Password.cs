using System.Security.Cryptography;
using System.Text;

namespace LogicitApp.Shared
{
    public class Password
    {
        public static string Hash(string password)
        {
            //using var sha1 = SHA1.Create();
            //var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
            return password; //Convert.ToHexString(hash).ToLower();
        }
    }
}
