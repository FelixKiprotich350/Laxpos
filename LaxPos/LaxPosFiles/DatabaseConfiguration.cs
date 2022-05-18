namespace LaxPos.LaxPosFiles
{
    using System;

    internal class DatabaseConfiguration
    {
        private readonly ClientDatabase DTB = new ClientDatabase();

        public string DBConnecString()
        {
            string server = this.DTB.Server;
            string user = this.DTB.User;
            string pass = this.DTB.Pass;
            string db = this.DTB.Db;
            string port = this.DTB.Port;
            string[] textArray1 = new string[11];
            textArray1[0] = "Server=";
            textArray1[1] = server;
            textArray1[2] = "; Uid =";
            textArray1[3] = user;
            textArray1[4] = "; Password =";
            textArray1[5] = pass;
            textArray1[6] = "; Database =";
            textArray1[7] = db;
            textArray1[8] = "; Port =";
            textArray1[9] = port;
            textArray1[10] = ";";
            return string.Concat(textArray1);
        }
    }
}

