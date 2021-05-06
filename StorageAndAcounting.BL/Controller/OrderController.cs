using StorageAndAcounting.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace StorageAndAcounting.BL.Controller
{
    public class OrderController : ControllerBase
    {
        private const string ORDERS_FILE_NAME = "Orders.dat";
        private const string BASKETS_FILE_NAME = "Baskets.dat";
        public Order CurrentOrder { get; }

        public Dictionary<Goods, int> CurrentBasket { get; set; }

        private readonly User CurrentUser;

        public List<Order> AllOrders { get; }

        public Dictionary<User, Dictionary<Goods, int>> AllBaskets { get; }

        public OrderController(User user)
        {
            if(user == null) throw new ArgumentNullException("User is NULL.", nameof(user));
            AllOrders = GetOrders();
            AllBaskets = GetBaskets();
            var tempOrd = AllOrders.FirstOrDefault(o => o.CurrentUser.Name == user.Name);
            if(tempOrd == null)
            {
                CurrentOrder = new Order(user);
                CurrentUser = user;
                CurrentBasket = new Dictionary<Goods, int>();
                AllOrders.Add(CurrentOrder);
                AllBaskets.Add(CurrentUser, CurrentBasket);
                SaveInfo();
            }
            else
            {
                foreach (var item in AllOrders)
                {
                    if (item.CurrentUser.Name == user.Name)
                    {
                        CurrentOrder = item;
                        CurrentUser = item.CurrentUser;
                    }
                }
                foreach (var item in AllBaskets)
                {
                    if (item.Key.Name == user.Name) CurrentBasket = item.Value;
                }
            }
        }

        public void Add(Goods goods, int amount)
        {
            if (goods == null) throw new ArgumentNullException("Product is null.", nameof(goods));
            if (amount <= 0) throw new ArgumentException("Amount should be positive.", nameof(amount));

            CurrentOrder.AddToBasket(goods.Id, amount);

            Goods temp = null;
            foreach (var item in CurrentBasket)
            {
                if (item.Key.Id == goods.Id) temp = item.Key;
            }
            if(temp == null)
            {
                CurrentBasket.Add(goods, amount);
            }
            else
            {
                CurrentBasket[temp] += amount;
            }

            SaveInfo();
        }

        // TODO: add method that deletes goods from basket

        private List<Order> GetOrders()
        {
            return Load<List<Order>>(ORDERS_FILE_NAME) ?? new List<Order>();
        }
        private Dictionary<User, Dictionary<Goods, int>> GetBaskets()
        {
            return Load<Dictionary<User, Dictionary<Goods, int>>>(BASKETS_FILE_NAME) ?? new Dictionary<User, Dictionary<Goods, int>>();
        }
        private void SaveInfo()
        {
            Save(BASKETS_FILE_NAME, AllBaskets);
            Save(ORDERS_FILE_NAME, AllOrders);
        }
    }
}
