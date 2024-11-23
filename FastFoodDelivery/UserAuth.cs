using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodDelivery
{
    public class UserAuth 
    {
        public int Id;
        public string Login;
        public string AccessToken;
        public DateTime? TimeRegister;
        public bool Admin;

        public UserAuth(int id,string login, string accesstoken, DateTime? timeregistration, bool admin) 
        {
            Id = id;
            Login = login;
            AccessToken = accesstoken;
            TimeRegister = timeregistration;
            Admin = admin;  

        }
        public UserAuth()
        {
           

        }
    }
}
