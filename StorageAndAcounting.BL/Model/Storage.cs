using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAndAcounting.BL.Model
{
    public class Storage
    {
        /// <summary>
        /// File name with all ID.
        /// </summary>
        private const string ALL_ITEMS_IDS = "Ids.txt";

        /// <summary>
        /// Items and their amount (id - amount).
        /// </summary>
        public Dictionary<string, int> AllItems { get; } 

        /// <summary>
        /// Orders queue.
        /// </summary>
        private List<Order> OrdersQueue { get; }

        /// <summary>
        /// List of ids to order.
        /// </summary>
        private List<string> ListForProvider { get; }


        public Storage()
        {
            OrdersQueue = new List<Order>();
            ListForProvider = new List<string>();
            AllItems = GetIds();
        }

        /// <summary>
        /// Reading dictionary of items and their amount from file.
        /// </summary>
        private Dictionary<string, int> GetIds()
        {
            var ids = new Dictionary<string, int>();
            using (var sr = new StreamReader(ALL_ITEMS_IDS))
            {
                while (!sr.EndOfStream)
                {
                    string[] row = sr.ReadLine().Split();

                    if (row.Length != 2) throw new ArgumentException("Wrong file.");

                    if (!int.TryParse(row[1], out int price)) throw new ArgumentException("Wrong file.");

                    ids.Add(row[0], price);
                }
            }
            return ids;
        }


        /// <summary>
        /// Method that checks all of the orders after delivery.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        public void CheckOrders(object obj, EventArgs args)
        {
            var temp = new List<Order>(OrdersQueue.ToArray());
            OrdersQueue.Clear();
            foreach (var item in temp)
            {
                if (!MakeOrder(item))
                {
                    OrdersQueue.Append(item);
                }
            }
        }

        /// <summary>
        /// Method that delivers items that are necessary.
        /// </summary>
        public void GetItemsFromProvider(object obj, EventArgs args)
        {
            foreach (var item in ListForProvider)
            {
                AllItems[item] += 100;
            }
            ListForProvider.Clear();
        }

        public int GetQueuePosition(User user)
        {
            int i = 1;
            foreach (var item in OrdersQueue)
            {
                if (item.CurrentUser.Name == user.Name) return i;
                i++;
            }
            return 0;
        }

        /// <summary>
        /// Сhecks whether the order can be fulfilled and executes if it can.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool MakeOrder(Order order)
        {
            bool IsEnough = true;

            var temp = new Dictionary<string, int>();

            foreach (var item in order.Basket)
            {
                temp.Add(item.Key, item.Value);
            }

            foreach (var item in temp)
            {

                if(AllItems[item.Key] < item.Value)
                {
                    order.Basket[item.Key] -= AllItems[item.Key];
                    AllItems[item.Key] = 0;

                    var tempGood = ListForProvider.LastOrDefault(x => x == item.Key);

                    if (tempGood == null)
                    {
                        ListForProvider.Add(item.Key);
                    }

                    IsEnough = false;
                }
                else
                {
                    AllItems[item.Key] -= item.Value;
                    if(AllItems[item.Key] == 0)
                    {
                        var tempGood = ListForProvider.LastOrDefault(x => x == item.Key);

                        if (tempGood == null)
                        {
                            ListForProvider.Add(item.Key);
                        }
                    }
                    order.Basket.Remove(item.Key);
                }
            }
            if (!IsEnough)
            {
                OrdersQueue.Add(order);
            }
            return IsEnough;
        }
    }
}
