using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        //burda kullanıcıya token ve ne kadar süresi kaldığını belirten bir yapı oluşturuyoruz 
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
