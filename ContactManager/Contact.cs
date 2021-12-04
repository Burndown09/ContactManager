using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    class Contact
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }

        public Contact(int ident, string fName, string lName, string pNumber, string elecMail)
        {
            this.id = ident;
            this.firstName = fName;
            this.lastName = lName;
            this.phoneNumber = pNumber;
            this.email = elecMail;
        }
        public Contact(string fName, string lName, string pNumber, string elecMail)
        {
            this.firstName = fName;
            this.lastName = lName;
            this.phoneNumber = pNumber;
            this.email = elecMail;
        }
    }
}
