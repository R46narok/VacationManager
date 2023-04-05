using System.Security.Cryptography;
using System.Text;

namespace Repositories.Helpers;

public static class Hasher
{
    public static string Hash(string password)
    {
        var data = Encoding.ASCII.GetBytes(password);
        var md5 = new MD5CryptoServiceProvider();
        var md5data = md5.ComputeHash(data);
        var hashedPassword = new ASCIIEncoding().GetString(md5data);
        md5.Dispose();
        return hashedPassword;
    }
}