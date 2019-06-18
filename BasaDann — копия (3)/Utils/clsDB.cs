using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasaDann.Classes;
using MySql.Data.MySqlClient;

namespace BasaDann.Utils
{
    public static class clsDB

    {

        public static MySqlConnection Get_DB_Connection()

        {
            string cn_String = Properties.Settings.Default.connection_String;

            MySqlConnection cn_connection = new MySqlConnection(cn_String);
            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            return cn_connection;
        }



        public static DataTable Get_DataTable(string SQL_Text)

        {
            MySqlConnection cn_connection = Get_DB_Connection();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter(SQL_Text, cn_connection);
            adapter.Fill(table);

            return table;

        }

        public static List<string> Execute_Reader_SQL()
        {
            List<string> p = new List<string>();
            MySqlConnection cn_connection = Get_DB_Connection();
            MySqlCommand cmd_Command = new MySqlCommand("SELECT name FROM zstatus", cn_connection);
            MySqlDataReader rdr = cmd_Command.ExecuteReader();
            while (rdr.Read())
            {
                var myString = rdr.GetString(0);
                p.Add(myString);
            }

            return p;
        }

        public static void Execute_SQL(string SQL_Text)

        {
            MySqlConnection cn_connection = Get_DB_Connection();
            MySqlCommand cmd_Command = new MySqlCommand(SQL_Text, cn_connection);
            cmd_Command.ExecuteNonQuery();

        }

        public static int Scalar_SQL(string req_text, int id) 

        {
            MySqlConnection cn_connection = Get_DB_Connection();
            MySqlCommand cmd_Command = new MySqlCommand(req_text + id, cn_connection);
            if (Convert.ToInt32(cmd_Command.ExecuteScalar()) > 0)
            {
                return Convert.ToInt32(cmd_Command.ExecuteScalar());
            }
            else
            {
                return 0;
            }
        }

        public static DataTable Get_Top_ID() // брать топ id из реквестов и сравнивать с предыдущим значением

        {
            DataTable dt = Get_DataTable(Requests.Req_Top_One);
            return dt;
        }





        public static void Close_DB_Connection()

        {
            string cn_String = Properties.Settings.Default.connection_String;
            MySqlConnection cn_connection = new MySqlConnection(cn_String);
            if (cn_connection.State != ConnectionState.Closed) cn_connection.Close();

        }


    }
}
