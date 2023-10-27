using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        //kullanıcı bilgilerini girdikten sonra  eğer bilgileri doğruysa bu operasyon çalışacak ve bu kullanıcının claim lerini bulup
        //Json Web Token üretip vericek
        AccessToken CreateToken(User user,List<OperationClaim> operationClaims);
    }
}
