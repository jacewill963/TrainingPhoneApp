using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace PhoneApp
{
    public static class AddressDA
    {
        static string conString = "Data Source=revature-cuny-jace-server.database.windows.net;Initial Catalog=Week2DB;Persist Security Info=True;User ID=jacewill963;Password=Khunta96#";
        static SqlConnection connect;
        static string command;
        static SqlCommand sqlCommand;
        static SqlDataReader data;

        public static void read(List<Address> addList, List<Country> ctryList)
        {
            //List<Country> countries = new List<Country>();
            using (connect = new SqlConnection(conString))
            {
                connect.Open();
                command = "SELECT * FROM Address";
                sqlCommand = new SqlCommand(command, connect);
                try
                {
                    //List<Address> addList = new List<Address>();
                    data = sqlCommand.ExecuteReader();
                    while (data.Read())
                    {
                        Address address = null;
                        foreach (var item in ctryList)
                        {
                            if (item.CountryID == (int)data[5])
                            {
                                address = new Address(
                                    (string)data[1],
                                    (string)data[2],
                                    (string)data[3],
                                    (int)data[4],
                                    (int)data[5],
                                    item);
                                break;
                            }
                        }
                        address.AddressID = (int)data[0];

                        addList.Add(address);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR DATA ACCESS - READ_ADDRESS: " + ex.Message);
                    Console.ReadLine();
                    throw;
                }
            }
        }

        public static void insert(Country c)
        {
            //Create Query Statment using Parmeterized SQL
            command = $"INSERT INTO dbo.Country (COUNTRY_NAME, COUNTRY_CODE) " +
                      $"VALUES (@countryName, @countryCode)";
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
                            sqlCommand.Parameters.AddWithValue("@countryName", c.CountryName);
                            sqlCommand.Parameters.AddWithValue("@countryCode", c.CountryCode);
                            sqlCommand.ExecuteNonQuery();
                        }

                        command = "SELECT MAX(COUNTRY_ID) FROM COUNTRY";
                        using (sqlCommand = new SqlCommand(command, connect))
                        {
                            using (data = sqlCommand.ExecuteReader())
                            {
                                while (data.Read()) { c.CountryID = (int)data[0]; }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR DATA ACCESS - INSERT_CONTACTS: " + ex.Message);
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
