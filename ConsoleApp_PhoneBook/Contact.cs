using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_PhoneBook {
    /// <summary>
    /// Object of contatas
    /// </summary>
    internal class Contact {
        public string Name { get; set; }
        public string Number { get; set; }

        public Contact(string name, string number)
        {
            Name = name;
            Number = number;
        }

    }
}
