namespace LaxPos.LaxPosFiles
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;

    public class DbTest
    {
        public bool DbConnectionTest()
        {
            bool flag2;
            try
            {
                MySqlConnection connection = new MySqlConnection(new DatabaseConfiguration().DBConnecString());
                if ((connection.State != ConnectionState.Open) && (connection.State != ConnectionState.Closed))
                {
                    flag2 = false;
                }
                else
                {
                    connection.Close();
                    connection.Open();
                    flag2 = true;
                }
            }
            catch
            {
                flag2 = false;
            }
            return flag2;
        }
    }
}

