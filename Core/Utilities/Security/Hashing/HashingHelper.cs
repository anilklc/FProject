using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //out keyword ile hashve salt dışarı vericez
        //bu void verdiğimiz  password değerinin salt ve hash değerini oluşturuyor
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt) 
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        //bu bool ise kontrol ediyor veri tabanındaki hash ile yukarıda oluşturduğumuz hash i karşılaştırıyoruz
        public static bool VerifyPasswordHash(string password,byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
