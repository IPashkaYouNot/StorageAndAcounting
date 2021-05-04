using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageAndAcounting.BL.Controller;
using StorageAndAcounting.BL.Model;


namespace StorageAndAcounting.CMD
{
    class Program
    {
        static void Main()
        {
            //var temp = new Wines();
            //Console.WriteLine(temp.GetPrice("Marlborough Sun"));

            #region user controlling
            Console.WriteLine("Register(0) or already registered(1)");
            int a = int.Parse(Console.ReadLine());
            UserController user;
            try
            {
                switch (a)
                {
                    case 0:
                        {
                            Console.Write("Type your name: ");
                            string name = Console.ReadLine();
                            Console.Write("Type your birth date: ");
                            string birthDate = Console.ReadLine();
                            Console.Write("Type your password: ");
                            string password = Console.ReadLine();
                            user = new UserController(name, birthDate, password);
                            Console.WriteLine("Account was created.");
                            Console.WriteLine(user.CurrentUser);
                            break;
                        }
                    case 1:
                        {
                            Console.Write("Type your name: ");
                            string name = Console.ReadLine();
                            Console.Write("Type your password: ");
                            string password = Console.ReadLine();
                            user = new UserController(name, password);
                            Console.WriteLine("You logged in.");
                            Console.WriteLine(user.CurrentUser);
                            break;
                        }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion
            
        }
    }
}
