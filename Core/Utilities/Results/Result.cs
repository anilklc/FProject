using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
       
        //constructor dışında set etmiyceğimiz için ve programcı kafasında kurmaması için sınırlandırıyoruz
        //this(success) ile bu constructordaki succesi alıp alttakinede gönder 
        //bu sayede sadece success istersem alttaki mesaj istersem her ikiside çalışcaktır
        public Result(bool success, string message):this(success)
        {
           Message = message;
          
        }
        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
