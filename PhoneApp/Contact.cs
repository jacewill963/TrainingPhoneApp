using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp
{
	class Contact
	{
		string fName, lName;
		string contactId;


		public Contact(string fName, string lName)
		{
			this.fName = fName;
			this.lName = lName;
			contactId = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
		}
		public override string ToString()
		{
			return "Contact Details";
		}
	}
}
