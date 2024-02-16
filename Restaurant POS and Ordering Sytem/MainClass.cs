using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Restaurant_POS_and_Ordering_Sytem
{
    class MainClass
    {
       public static readonly string con_string = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
        public static MySqlConnection con = new MySqlConnection(con_string);


        //Method  to check  user  validation


        public static MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(con_string);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                // Handle connection error
                Console.WriteLine("Error connecting to the database: " + ex.Message);
            }

            return connection;
        }

        public static void LoadData(MySqlCommand cmd, DataGridView dgv, ListBox lb)
        {
           
    }

    

    }
}