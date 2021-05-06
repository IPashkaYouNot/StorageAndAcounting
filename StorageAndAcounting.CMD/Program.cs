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
        static void ChooseOption(out int choose)
        {
            Console.WriteLine("Type 1 if you want to log in, type 2 to register or type 0 to log in as Guest.");
            while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out choose))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please type number symbol.");
                Console.ResetColor();
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Type 1 if you want to log in, type 2 to register or type 0 to log in as Guest.");
            }
            Console.WriteLine();
        }
        static UserController ChangeUser()
        {
            UserController user = null;
            Console.Clear();
            while (user == null)
            {
                
                int choose;
                do
                {
                    ChooseOption(out choose);
                    if (choose < 0 || choose > 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please type correct option.");
                        Console.ResetColor();
                    }
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();

                } while (choose < 0 || choose > 2);

                try
                {
                    switch (choose)
                    {
                        case 1:
                            {
                                Console.Write("Type your name: ");
                                string name = Console.ReadLine();
                                Console.Write("Type your password: ");
                                string password = Console.ReadLine();
                                user = new UserController(name, password);
                                Console.Clear();
                                Console.WriteLine("You successfully logged in.");
                                Console.WriteLine(user.CurrentUser);
                                break;
                            }
                        case 2:
                            {
                                Console.Write("Type your name: ");
                                string name = Console.ReadLine();
                                Console.Write("Type your birth date: ");
                                string birthDate = Console.ReadLine();
                                Console.Write("Type your password: ");
                                string password = Console.ReadLine();
                                user = new UserController(name, birthDate, password);
                                Console.Clear();
                                Console.WriteLine("Account was created.");
                                Console.WriteLine(user.CurrentUser);
                                break;
                            }
                        default:
                            {
                                user = new UserController();
                                Console.WriteLine(user.CurrentUser);
                                break;
                            }
                    }
                } 
                catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                }

            }
            return user;
        }
        static void Main()
        {
            var user = ChangeUser();
            
        }
    }
}
