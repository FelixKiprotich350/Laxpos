namespace LaxPos.LaxPosFiles
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class UserLoggedIn
    {
        public List<string> UserRights = new List<string>();

        public UserLoggedIn()
        {
            this.UserID = "";
            this.UserFirstname = "";
            this.UserLastname = "";
            this.UserRole = "";
            this.UserRights.Clear();
        }

        public string UserID { get; set; }

        public string UserFirstname { get; set; }

        public string UserLastname { get; set; }

        public string UserRole { get; set; }
    }
}

