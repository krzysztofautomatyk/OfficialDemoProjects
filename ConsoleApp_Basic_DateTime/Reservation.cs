using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Basic_DateTime
{
    public class Reservation
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public Reservation(DateTime @from, DateTime to)
        {
            From = @from;
            To = to;
        }
    }
}
