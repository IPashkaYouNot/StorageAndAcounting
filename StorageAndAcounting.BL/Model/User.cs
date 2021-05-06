using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAndAcounting.BL.Model
{
    [Serializable]
    public class User
    {
        /// <param name="userName"> Name of User. </param>
        /// <exception cref="ArgumentNullException"> Thrown if name is empty or null. </exception>
        public User(string userName)
        {
            if(string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("Name is empty or null.", nameof(userName));
            Name = userName;
        }

        /// <param name="userName"> Name of User. </param>
        /// <param name="password"> Password of User. </param>
        /// <param name="birthDate"> Birth date of User. </param>
        /// <exception cref="ArgumentNullException"> Thrown if name, password or birth date are empty or null. </exception>
        /// <exception cref="ArgumentException"> Thrown if birth date is less than 14 or more than 150. </exception>
        /// <exception cref="FormatException"> Thrown if birth data contains non valid presentation. </exception>
        public User(string userName, string password, string birthDate)
        {
            var birthdate = DateTime.Parse(birthDate);
            Age = DateTime.Today.Year - birthdate.Year;
            if (birthdate > DateTime.Today.AddYears(-Age)) Age--;

            #region Data validation
            if (Age <= 14 || Age >= 150) throw new ArgumentException("Date of birth is invalid.");

            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException(nameof(userName), "Name is empty or null.");

            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password), "Password is empty or null.");
            #endregion

            Name = userName;
            Password = password;
        }

        /// <summary>
        /// Checks if password is correct.
        /// </summary>
        /// <param name="password"> Password of User. </param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            if(string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("Password is empty or null.", nameof(password));
            return Password == password;
        }

        /// <summary>
        /// Name of User.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Age of User.
        /// </summary>
        public int Age { get; }

        /// <summary>
        /// Password of User.
        /// </summary>
        private string Password { get; set; }

        public override string ToString()
        {
            return $"Hello, {Name} ;)";
        }
    }
}
