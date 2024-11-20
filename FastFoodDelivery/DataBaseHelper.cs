using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;


namespace FastFoodDelivery
{
    public class DataBaseHelper
    {

        public static bool Registration(string login, string password, DateTime time)
        {
            Debug.WriteLine("Registration try");

            using (ApplicationContext db = new ApplicationContext())
            {
               
                if (db.Users.Any(u => u.Login == login))
                {
                    Debug.WriteLine("User already exists!");                    
                    return false;

                }
                else 
                {
                    string passwordHash = ComputeSha256Hash(password);
                    User user1 = new User { Login = login, Password = passwordHash, TimeRegister = time };

                    db.Users.Add(user1);
                    db.SaveChanges();
                    return true;
                }

               
            }
        }
        public static byte Login(string login, string password)
        {
            Debug.WriteLine("Login attempt");
            using (ApplicationContext db = new ApplicationContext())
            {

                var user = db.Users.SingleOrDefault(u => u.Login == login);

                if (user != null)
                {
                    string passwordHash = ComputeSha256Hash(password);
                    if (user.Password == passwordHash)
                    {
                        Debug.WriteLine("Login successful!");
                        return 1;
                    }
                    else
                    {
                        Debug.WriteLine("Invalid password!");
                        return 52;
                    }
                }
                else
                {
                    Debug.WriteLine("User not found!");
                    return 2; 
                }
            }
        }
        public static string ComputeSha256Hash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        
    }
}
