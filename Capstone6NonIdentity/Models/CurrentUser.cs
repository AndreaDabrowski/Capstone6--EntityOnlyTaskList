using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone6NonIdentity.Models
{
    public class CurrentUser
    {
        public CurrentUser(bool logIn, int iD)
        {
            LogIn = logIn;
            ID = iD;
        }
        public CurrentUser (bool logIn)
        {
            LogIn = logIn;
            ID = 0;
        }
        public CurrentUser(int iD)
        {
            LogIn = false;
            ID = iD;
        }
        public CurrentUser()
        {
            LogIn = false;
            ID = 1;
        }
        public bool LogIn { get; set; }
        public int ID { get; set; }
    }
}