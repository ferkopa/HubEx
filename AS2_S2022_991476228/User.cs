/**
 * @author:  Paulina Ferko (student num. 991476228)
 * @date:    August 8th, 2022
 * File: User.cs
 * Solution Name: AS2_FerkoS2022
 * 
 * @details: A class designed to hold user's name when entered on the main page
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS2_FerkoS2022
{
    /// <summary>
    ///     custom class to hold the current user's name
    /// </summary>
    public class User
    {
        private string _username;
        
        /// <summary>
        ///     Default constructor
        /// </summary>
        public User()
        {
            _username = "";
        }

        /// <summary>
        ///     Constructor with a string parameter for the username
        /// </summary>
        /// <param name="username"></param>
        public User(string username)
        {
            _username = username;
        }
        
        /// <summary>
        ///     Accessors and Mutators for the username
        /// </summary>
        public string Username { get; set; }
    }

}
