﻿using StorageAndAcounting.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace StorageAndAcounting.BL.Controller
{
    public class UserController
    {
        /// <summary>
        /// List of registered users.
        /// </summary>
        public List<User> Users { get; }

        /// <summary>
        /// Current User.
        /// </summary>
        public User CurrentUser { get; }

        /// <summary>
        /// Variable to check if user is Guest.
        /// </summary>
        private bool IsGuest { get; } = true;

        /// <summary>
        /// Constructor if Guest logged in.
        /// </summary>
        /// <exception cref="ArgumentNullException"> Thrown if name are empty or null. </exception>
        public UserController()
        {
            CurrentUser = new User("Guest");
        }

        /// <summary>
        /// Constructor for registered User.
        /// </summary>
        /// <param name="userName"> Name of user. </param>
        /// <param name="password"> Password of user. </param>
        /// <exception cref="ArgumentNullException"> Thrown if name or password are empty or null. </exception>
        /// <exception cref="ArgumentException"> Thrown if name of user was not found or password was incorrect. </exception>
        public UserController(string userName, string password)
        {
            #region Data validation
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("Name is empty or NULL.", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Password is empty or NULL.", nameof(userName));
            }
            #endregion

            Users = GetUsersData();

            User currentUser = null;

            foreach (var item in Users)
            {
                if (item.Name == userName) currentUser = item;
            }

            if(currentUser == null)
            {
                throw new ArgumentException("User was not found.", nameof(currentUser));
            }

            if (!currentUser.CheckPassword(password))
            {
                throw new ArgumentException("Password is incorrect.", nameof(password));
            }
            IsGuest = false;
            CurrentUser = currentUser;
        }

        /// <summary>
        /// Constructor for a new User.
        /// </summary>
        /// <param name="userName"> Name of User. </param>
        /// <param name="birthDate"> Birth date of User. </param>
        /// <param name="password"> Password of User. </param>
        /// <exception cref="ArgumentException"> Thrown if User with same name exists or birth date is less than 14 or more than 150. </exception>
        /// <exception cref="ArgumentNullException"> Thrown if name, password or birth date are empty or null. </exception>
        /// <exception cref="FormatException"> Thrown if birth data contains non valid presentation. </exception>
        public UserController(string userName, string birthDate, string password)
        {
            Users = GetUsersData();
            User currentUser = null;
            foreach (var item in Users)
            {
                if (item.Name == userName) currentUser = item;
            }

            if (currentUser != null)
            {
                throw new ArgumentException("This User already exists.", nameof(userName));
            }

            IsGuest = false;
            CurrentUser = new User(userName, password, birthDate);
            Users.Add(CurrentUser);
            Save();
        }

        /// <summary>
        /// Getting users list form file.
        /// </summary>
        /// <returns> Users list. </returns>
        private List<User> GetUsersData()
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream("Users.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    if (formatter.Deserialize(fs) is List<User> users)
                    {
                        return users;
                    }
                }
                return new List<User>();
            }
        }

        /// <summary>
        /// Save User's data into a file.
        /// </summary>
        private void Save()
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("Users.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Users);
            }
        }
    }
}
