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
	[Serializable]
	class Phone: ISerializable
	{
		//Attributes
		internal string phoneName { get; set; }
		internal List<Contact> contactList;
		
		
		//Constructors
		public Phone(string phoneName)
		{
			this.phoneName = phoneName;
		}
		
		public Phone(SerializationInfo info, StreamingContext context)
		{
			phoneName = (string)info.GetValue("PhoneName", typeof(string));
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
		//Serialize Method
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("PhoneName", phoneName);
		}
	}
}
