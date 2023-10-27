using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorResult :Result
    {
        //base yandaki result  için kullandıldı this olsaydı errosresultta olurdu
        public ErrorResult(string message):base(false,message) 
        { 
        }
        public ErrorResult() : base(false)
        {

        }
    }
}
