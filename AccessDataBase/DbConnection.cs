using System.Data.OleDb;

namespace StudingWorkloadCalculator.AccessDataBase
{
    public class DbConnection
    {
        public static OleDbConnection cn = new OleDbConnection();
        public static OleDbConnectionStringBuilder connect = new OleDbConnectionStringBuilder();
        public static OleDbCommand myCommand;
        private static bool isOpen;

        public static void OpenConnection()
        {
            if (isOpen)
            {
                return;
            }

            connect.Provider = "Microsoft.ACE.OLEDB.12.0";
            connect.DataSource = @"BD.mdb";
            cn.ConnectionString = connect.ConnectionString + ";";
            cn.Open();
            isOpen = true;
        }

        public static void CloseConnection()
        {
            isOpen = false;
            cn.Close();
        }
    }
}
