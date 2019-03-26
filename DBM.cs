using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Kitness1
{
    class DBM
    {
        String strURL = "server = 127.0.0.1; uid = sa; pwd = inha1958; database = Kitness;";
        public SqlConnection DB_con;
        public SqlCommand DB_stmt;
        public SqlDataReader DB_rs;
        
        public void dbOpen()
        {
            try {
                //Class.forName(strDriver); //Load JDBC-ODBC bridge driver
                DB_con = new SqlConnection(strURL);
                DB_stmt = new SqlCommand();
                DB_stmt.Connection = DB_con;
                DB_stmt.CommandText = "SELECT * FROM Member";
                DB_con.Open();

            } catch (Exception e) {
                Console.WriteLine("SQLException : "+e.Message);
            }
        }

        public void dbClose()
        {
            try {
                DB_con.Close();
            } catch (Exception e) {
                Console.WriteLine("SQLException : "+e.Message);
            }
}
    }
}
