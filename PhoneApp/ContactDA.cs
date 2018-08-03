using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace PhoneApp
{
    public static class ContactDA
    {
        static string conString = "Data Source=revature-cuny-jace-server.database.windows.net;Initial Catalog=Week2DB;Persist Security Info=True;User ID=jacewill963;Password=Khunta96#";
        static SqlConnection connect;
        static string command;
        static SqlCommand sqlCommand;
        static SqlDataReader data;

        static public List<Contact> read(List<Address> addList)
        {
            using (connect = new SqlConnection(conString))
            {
                connect.Open();
                command = "SELECT * FROM Contacts";
                sqlCommand = new SqlCommand(command, connect);
                try
                {
                    List<Contact> conList = new List<Contact>();
                    data = sqlCommand.ExecuteReader();
                    while (data.Read())
                    {
                        Contact contact = null;
                        foreach (var item in addList)
                        {
                            if (item.AddressID == (int)data[5])
                            {
                                contact = new Contact(
                                    (string)data[1],
                                    (string)data[2],
                                    (int)data[3],
                                    (string)data[4],
                                    (int)data[5],
                                    (long)data[6],
                                    item);
                            }
                        }
                        if (contact == null) { contact = new Contact(); }
                        contact.ContactId = (int)data[0];
                        conList.Add(contact);
                    }
                    return conList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR DATA ACCESS - READ_CONTACTS: " + ex.Message);
                    Console.ReadLine();
                    throw;
                }
            }
        }

        public static void insert(Contact c)
        {
            //Create Query Statment using Parmeterized SQL
            command = $"INSERT INTO dbo.Contacts (FIRST_NAME, LAST_NAME, AGE, GENDER, ADDRESS_ID, PHONE_NUMBER) " +
                      $"VALUES (@firstName, @lastName, @age, @gender, @addressID, @phoneNumber)";
            //Establish a New Connection
            using (connect = new SqlConnection(conString))
            {
                connect.Open();
                //Establish an Adapter to write the data
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    try
                    {
                        //Establish an Command using SQLCommand
                        using (sqlCommand = new SqlCommand(command, connect))
                        {
                            //Execute Query on the database 
                            sqlCommand.Parameters.AddWithValue("@firstName", c.FName);
                            sqlCommand.Parameters.AddWithValue("@lastName", c.LName);
                            sqlCommand.Parameters.AddWithValue("@age", c.Age);
                            sqlCommand.Parameters.AddWithValue("@gender", c.Gender);
                            sqlCommand.Parameters.AddWithValue("@addressID", c.AddressID);
                            sqlCommand.Parameters.AddWithValue("@phoneNumber", c.PhoneNumber);
                            sqlCommand.ExecuteNonQuery();
                        }
                        command = "SELECT MAX(CONTACT_ID) FROM CONTACTS";
                        using (sqlCommand = new SqlCommand(command, connect))
                        {
                            using (data = sqlCommand.ExecuteReader())
                            {
                                while (data.Read()) { c.ContactId = (int)data[0]; }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR DATA ACCESS - READ_CONTACTS: " + ex.Message);
                        throw;
                    }
                };
            }

            /*List<Country> srchResults = new List<Country>();
            using (connect = new SqlConnection(conString))
            {
                connect.Open();
                using (sqlCommand = new SqlCommand(searchStr))
                {

                }
            }*/
        }
    }
}
