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

        static void PrintBrands(List<string> brands)
        {
            int i = 1;
            foreach (var item in brands)
            {
                Console.WriteLine(i + ") " + item);
                i++;
            }
        }


        static string GetBrand(Items item, int choose)
        {
            int i = 1;
            foreach (var brand in GoodsController.GetBrands(item))
            {
                if (i == choose) return brand;
                i++;
            }
            throw new ArgumentException("Incorrect option.");
        }


        static void ChooseLimits(int low, int high, string message, out int choose)
        {
            do
            {
                ChooseOption(out choose, message);
                if (choose < low || choose > high)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please type correct option.");
                    Console.ResetColor();
                }

            } while (choose < low || choose > high);
        }


        static void MakeOrder(OrderController orderController, Storage storage)
        {
            bool finished = false;
            while (!finished)
            {
                Console.Clear();
                PrintBasket(orderController.CurrentBasket);
                Console.WriteLine($"1) Add more items.");
                Console.WriteLine($"2) Send order.");
                Console.WriteLine($"3) Exit from basket.");
                ChooseLimits(1, 3, "Your choose: ", out int choose);
                Console.WriteLine();
                try
                {
                    switch (choose)
                    {
                        case (1):
                            bool chosen = false;
                            while (!chosen)
                            {
                                try
                                {
                                    var quantity = Enum.GetNames(typeof(Items)).Length;
                                    Console.Clear();
                                    Console.WriteLine("What type of product you want?");
                                    foreach (var item in (Items[])Enum.GetValues(typeof(Items)))
                                    {
                                        Console.WriteLine((int)item + ") " + item);
                                    }
                                    ChooseLimits(1, quantity, "Your chosen item: ", out int good);
                                    PrintBrands(GoodsController.GetBrands((Items)good));
                                    ChooseLimits(1, GoodsController.GetAmount((Items)good), "Choose what you want: ", out int brand);
                                    ChooseLimits(1, 100, "Type amount (less than 100): ", out int amount);
                                    orderController.Add(GoodsController.CreateItem((Items)good, GetBrand((Items)good, brand)), amount);
                                    chosen = true;
                                }
                                catch (TypeInitializationException ex)
                                {
                                    Console.WriteLine(ex.InnerException);
                                }
                                catch(Exception ex)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(ex.Message);
                                    Console.ResetColor();
                                }
                            }
                            break;
                        case (2):
                            orderController.FinishOrder();
                            if (storage.MakeOrder(orderController.CurrentOrder)) Console.WriteLine("Your order is completed!");
                            else
                            {
                                Console.WriteLine("You was added to the queue. Come back next day.");
                                Console.WriteLine($"You are on {storage.GetQueuePosition(orderController.CurrentOrder.CurrentUser)} position in queue.");
                            }
                            orderController.CheckCurrentOrder();

                            finished = true;
                            break;
                        default:
                            finished = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }

        }
        

        static void Greetings()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("Welcome to Pablo Shop   ͡° ͜ʖ ͡°  \n");
            Console.WriteLine("To continue you should log in or register.");
            Console.OutputEncoding = Encoding.Default;
        }


        static void ChooseOption(out int choose, string message)
        {
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out choose))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please type number symbol.");
                Console.ResetColor();
                Console.Write(message);
            }
        }


        static void ChangeUser(UserController user, OrderController orderController)
        {
            bool sucess = false;
            while (!sucess)
            {

                ChooseLimits(1, 2, "Log in(1) or register(2): ", out int choose);
                Console.WriteLine();
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
                                sucess = user.ChangeUser(name, password);
                                Console.WriteLine();
                                Console.WriteLine("You successfully logged in.");
                                Console.WriteLine(user.CurrentUser);
                                orderController.ChangeUser(user.CurrentUser);
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
                                sucess = user.RegisterUser(name, birthDate, password);
                                Console.WriteLine();
                                Console.WriteLine("Account was created.");
                                Console.WriteLine(user.CurrentUser);
                                orderController.ChangeUser(user.CurrentUser);
                                break;
                            }
                    }
                }
                // TODO: catch not all exceptions.
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
            Console.WriteLine("\nLets start shopping!");
        }


        private static void PrintBasket(Dictionary<Goods, int> currentBasket)
        {
            if (currentBasket.Count == 0) { Console.WriteLine("Seems like your basket is empty!\n");  return; }
            Console.WriteLine("Your current basket:");
            foreach (var item in currentBasket)
            {
                Console.WriteLine(item.Key.Brand + " " +  item.Value);
            }
            Console.WriteLine();
        }


        private static void PrintBasket(Dictionary<string, int> currentBasket)
        {
            if (currentBasket.Count == 0) { Console.WriteLine("Seems like your order is already completed!\n"); return; }

            Console.WriteLine("Your current unfinished order:");
            foreach (var item in currentBasket)
            {
                Console.WriteLine(GoodsController.GetBrand(item.Key) + " " + item.Value);
            }
            Console.WriteLine();
        }


        static void Menu()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("\tMenu  ͡° ͜ʖ ͡°    .\n");
            Console.OutputEncoding = Encoding.Default;
            Console.WriteLine("Change account(1)");
            Console.WriteLine("Add items to your basket(2)");
            Console.WriteLine("Check your order(3)");
            Console.WriteLine("Check your basket(4)");
            Console.WriteLine("Move to next day(5)");
            Console.WriteLine("Finish program(6)\n");
        }

        static void Main()
        {
            #region initializing all necessary objects
            GoodsController goodsController = new GoodsController();

            Greetings();
            var userController = new UserController();
            var orderController = new OrderController();

            var storage = new Storage();
            var calendar = new Calendar();


            ChangeUser(userController, orderController);

            

            // TODO: add information about completed orders (for current user)
            calendar.NewDay += storage.GetItemsFromProvider;
            calendar.NewDay += storage.CheckOrders;
            calendar.NewDay += orderController.CheckOrders;

            #endregion

            while (true)
            {
                Console.Write("Type any key to move to the menu . . .");
                Console.ReadKey();
                Console.Clear();
                Menu();
                ChooseLimits(1, 6, "Type your option: ", out int choose);
                Console.WriteLine();
                switch (choose)
                {
                    case (1):
                        ChangeUser(userController, orderController);
                        break;
                    case (2):
                        MakeOrder(orderController, storage);
                        break;
                    case (3):
                        if (orderController.CurrentOrder.IsSent)
                        {
                            Console.WriteLine($"You are on {storage.GetQueuePosition(orderController.CurrentOrder.CurrentUser)} position in queue.");
                        }
                        else Console.WriteLine("Unsent order.");
                        PrintBasket(orderController.CurrentOrder.Basket);
                        break;
                    case (4):
                        PrintBasket(orderController.CurrentBasket);
                        break;
                    case (5):
                        calendar.MoveOneDay();
                        break;
                    case (6):
                        return;
                }
            }
        }
    }
}