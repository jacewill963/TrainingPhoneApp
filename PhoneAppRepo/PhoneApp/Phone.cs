using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
namespace PhoneApp
{
	class Phone
	{
        //Attributes
        string phoneName;
        string fname;
        string lname;
        string email;
        long phoneNumber;
        public string PhoneName { get => phoneName; set => phoneName = value; }
        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public string Email { get => email; set => email = value; }
        public long PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        internal List<Contact> contactList;


        //Constructors
        public Phone(){}
		public Phone(string phoneName, string fname, string lname, string email, long phoneNumber)
		{
			this.PhoneName = phoneName;
            this.Fname = fname;
            this.Lname = lname;
            this.email = email;
            this.phoneNumber = phoneNumber;
		}

		//Methods
		internal void addContact(Contact contact)
		{
			contactList.Add(contact);
		}
		internal void shutDown() // Method to turn phone off or exit application
		{
			try
			{
				System.Environment.Exit(0);
			}catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
