using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBase
{
    class ClassPay
    {
        public int ID { get; set; }
        public int Code { get; set; }
        public string FIO { get; set; }
        public string PostStaff { get; set; }
        public DateTime? DatePay { get; set; }
        public decimal Summa { get; set; }
    }
}
