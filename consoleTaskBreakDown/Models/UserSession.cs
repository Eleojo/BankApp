using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Classes
{
    public static class UserSession
    {
        public static User LoggedInUser {  get; set; }
        public static User RegisteredUser { get; set; }

    }
}
