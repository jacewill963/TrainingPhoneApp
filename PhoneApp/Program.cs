using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PhoneApp
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Phone myPhone;
				//Contact sampleContact = new Contact("Sample", "Contact");
				Stream stream;
				if (File.Exists("PhoneData.dat")) //Check for existing files in the directory.
				{
					//If file exists open and process data to myPhone.
					stream = File.Open("PhoneData.dat", FileMode.Open);
					BinaryFormatter BF = new BinaryFormatter();
					myPhone = (Phone)BF.Deserialize(stream);
					stream.Close();

					Console.WriteLine($"Accessing {myPhone.phoneName}");
				}
				else
				{
					//If file doesn't exist create a new file and store information entered by user.
					Console.WriteLine(":::::New Phone Creation:::::");
					Console.WriteLine();
					Console.Write("Please enter your name:");
					var name = Console.ReadLine();
					myPhone = new Phone($"{name}'s Phone");

					stream = File.Open("PhoneData.dat", FileMode.Create);
					BinaryFormatter BF = new BinaryFormatter();
					BF.Serialize(stream, myPhone);
					stream.Close();
					Console.WriteLine("Your Phone have been Created.");
				}
				bool display = true;
				string currDisplay = "main";
				string currMenu = ":::::Main Menu:::::" + System.Environment.NewLine + "(1) Manage Contacts" + System.Environment.NewLine + "(2) Phone Logs" + System.Environment.NewLine + "(3) Factory Reset" + System.Environment.NewLine + "(4) Turn Off" + System.Environment.NewLine + ":::::Main Menu:::::";
				int menuSel;

				while (display == true && currDisplay != null)
				{
					Console.WriteLine(currMenu);
					menuSel = Convert.ToInt16(Console.ReadLine());

					if (currDisplay == "main")
					{
						switch (menuSel)
						{
							case 1:
								currMenu = ":::::Manage Contacts:::::" + System.Environment.NewLine + "(1) View Contacts" + System.Environment.NewLine + "(2) Add Contacts" + System.Environment.NewLine + "(3) Update Contact" + System.Environment.NewLine + "(4) Delete Contact" + System.Environment.NewLine + "(5) Cancel" + System.Environment.NewLine + ":::::Manage Contacts:::::";
								currDisplay = "contacts";
								continue;
							case 2:
								currMenu = ":::::Phone Logs:::::" + System.Environment.NewLine + "(1) View Phone Logs" + System.Environment.NewLine + "(2) Delete Phone Log Entry" + System.Environment.NewLine + "(3) Clear Phone Logs" + System.Environment.NewLine + "(4) Cancel" + System.Environment.NewLine + ":::::Phone Logs:::::";
								currDisplay = "phonelogs";
								continue;
							case 3:
								currMenu = ":::::Factory Reset:::::" + System.Environment.NewLine + "(1) Reset Phone to Factory Settings" + System.Environment.NewLine + "(2) Cancel" + System.Environment.NewLine + ":::::Factory Reset:::::";
								currDisplay = "reset";
								continue;
							case 4:
								currMenu = ":::::Shut Down:::::" + System.Environment.NewLine + "(1) Are you sure you want to shut down?" + System.Environment.NewLine + "(2) Cancel" + System.Environment.NewLine + ":::::Shut Down:::::";								currDisplay = "reset";
								continue;
							default:
								Console.WriteLine("Selection entered wasn't valid, please select options 1-4.");
								continue;
						}
					}
					else if(currDisplay == "contacts")
					{
						switch (menuSel)
						{
							case 1:
								//Execute ViewContacts call to Phone obj
								if(myPhone.contactList.Count == 0){Console.WriteLine("No Contacts Saved to Display");continue; }
								foreach(Contact contact in myPhone.contactList){contact.ToString();}
								continue;
							case 2:
								currMenu = ":::::Phone Logs:::::" + System.Environment.NewLine + "(1) View Phone Logs" + System.Environment.NewLine + "(2) Delete Phone Log Entry" + System.Environment.NewLine + "(3) Clear Phone Logs" + System.Environment.NewLine + "(4) Cancel" + System.Environment.NewLine + ":::::Phone Logs:::::";
								currDisplay = "phonelogs";
								continue;
							case 3:
								currMenu = ":::::Factory Reset:::::" + System.Environment.NewLine + "(1) Reset Phone to Factory Settings" + System.Environment.NewLine + "(2) Cancel" + System.Environment.NewLine + ":::::Factory Reset:::::";
								currDisplay = "reset";
								continue;
							case 4:
								currMenu = ":::::Shut Down:::::" + System.Environment.NewLine + "(1) Are you sure you want to shut down?" + System.Environment.NewLine + "(2) Cancel" + System.Environment.NewLine + ":::::Shut Down:::::"; currDisplay = "reset";
								continue;
							default:
								Console.WriteLine("Selection entered wasn't valid, please select options 1-4.");
								continue;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			Console.ReadLine();
		}
	}
}
