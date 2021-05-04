using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAndAcounting.BL.Model
{
    public class Order
    {
        /// <summary>
        /// List of all goods of User with their ids.
        /// </summary>
        public Dictionary<string, int> Basket { get; }
        /// <summary>
        /// Current User.
        /// </summary>
        public User CurrentUser { get; }
        /// <summary>
        /// Variable if user is registered or guest.
        /// </summary>
        public bool IsRegistered { get; }

        /// <param name="currentUser"> Current User. </param>
        /// <exception cref="ArgumentNullException"> Thrown if current user is null. </exception>
        public Order(User currentUser)
        {
            CurrentUser = currentUser ?? throw new ArgumentNullException("User is null.", nameof(currentUser));
            Basket = new Dictionary<string, int>();
        }

        /// <summary>
        /// Adds to basket product by it's id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        public void AddToBasket(string id, int amount)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException("Id is null.", nameof(id));
            if (amount <= 0) throw new ArgumentException("Amount has to be positive.", nameof(amount));
            if(Basket.TryGetValue(id, out _))
            {
                Basket[id] += amount;
            }
            else
            {
                Basket.Add(id, amount);
            }
        }
    }
}
