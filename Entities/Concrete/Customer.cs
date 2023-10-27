using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Customer : IEntity
    {
        //veritabanımzıda int yerine string verilmiş o yüzden string
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CompanyName { get; set; }
        public string? City { get; set; }
    }
}
