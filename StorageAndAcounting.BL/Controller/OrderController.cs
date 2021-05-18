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
    public class OrderController : ControllerBase, IUserChangeable
    {
        /// <summary>
        /// Order of current user.
        /// </summary>
        public Order CurrentOrder { get; private set; }

        public Dictionary<Goods, int> CurrentBasket { get; private set; }

        private User CurrentUser;

        public List<Order> AllOrders { get; private set; }

        public Dictionary<User, Dictionary<Goods, int>> AllBaskets { get; private set; }

        public OrderController()
        {
            AllOrders = new List<Order>();
            AllBaskets = new Dictionary<User, Dictionary<Goods, int>>();
        }

        public bool ChangeUser(User user)
        {
            if(user == null) throw new ArgumentNullException("User is NULL.", nameof(user));

            var tempOrd = AllOrders.FirstOrDefault(o => o.CurrentUser.Name == user.Name);

            if(tempOrd == null)
            {
                CurrentOrder = new Order(user);
                CurrentUser = user;
                CurrentBasket = new Dictionary<Goods, int>();
                AllOrders.Add(CurrentOrder);
                AllBaskets.Add(CurrentUser, CurrentBasket);
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
            return true;
        }

        public void FinishOrder()
        {
            CurrentOrder.IsSent = true;
            CurrentBasket.Clear();
        }

        public bool CheckCurrentOrder()
        {
            var temp = CurrentOrder.IsEmpty();
            if (temp)
            {
                AllOrders.Remove(CurrentOrder);
                CurrentOrder = new Order(CurrentUser);
                AllOrders.Add(CurrentOrder);
            }
            return temp;
        }


        public void CheckOrders(object obj, EventArgs args)
        {
            var temp = new List<Order>(AllOrders.ToArray());

            foreach (var item in temp)
            {
                if (item.IsEmpty())
                {
                    if (item == CurrentOrder)
                    {
                        CurrentOrder = new Order(CurrentUser);
                        AllOrders.Remove(item);
                        AllOrders.Add(CurrentOrder);
                    }
                    else
                    {
                        AllOrders.Remove(item);
                        var order = new Order(item.CurrentUser);
                        AllOrders.Add(order);
                    }
                }
            }
        }

        public void Add(Goods goods, int amount)
        {
            if (goods.IsEighteen && CurrentUser.Age < 18) throw new ArgumentException("You are not old enough to order alcohol.");
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
        }


        public void Delete(Goods goods)
        {
            if (goods == null) throw new ArgumentNullException("Product is null.", nameof(goods));

            Goods temp = null;
            foreach (var item in CurrentBasket)
            {
                if (item.Key.Id == goods.Id) temp = item.Key;
            }
            if(temp == null)
            {
                throw new ArgumentException("There is no this type of product in basket.");
            }
            CurrentBasket.Remove(temp);
        }

    }
}
