/**
 * @author:  Paulina Ferko (student num. 991476228)
 * @date:    August 8th, 2022
 * File: MainWindow.xaml.cs
 * Solution Name: AS2_S2022_991476228
 * 
 * @details: WPF code-behind to handle controls for MainWindow.xaml window.
 *           Code handles accepting username as input and generates new window to display 
 *           if the username is not left empty or null.
 *           
 * @reference:
 *      Microsoft Docs. (2021). How to open a window - WPF .NET [Source Code].
 *          https://docs.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-window-dialog-box?view=netdesktop-6.0
 */

using AS2_FerkoS2022.xamlpages;
using System.Windows;

namespace AS2_FerkoS2022
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       /// <summary>
       ///      Default constructor to launch window and place focus on text box
       /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //places focus on the first textbox
            txtUsername.Focus();
        }
        
        /// <summary>
        ///     Method to handle button control upon clicking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_click(object sender, RoutedEventArgs e)
        {
            //string to hold username from textbox
            string username = txtUsername.Text;

            //new User object instance
            User user = new User();

            //message to be displayed if error occurs
            string message = "Username cannot be empty. Please enter a username";

            //error handling if user does not input a name
            if (username.Equals("") || username == null)
            {
                //error message displays in form of popup window
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {   
                //sets username from textbox input to User instance
                user.Username = username;

           /**
             * Creating new instance of window and displaying it as a new window on screen.
             * Uses parameter constructor to pass the username from current window to another window
             * 
             * Code Reference:
             *    Title: How to open a window - WPF .NET
             *    Author: Microsoft Docs
             *    Date: 2021
             *    Code version: N/A
             *    Availability:  https://docs.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-window-dialog-box?view=netdesktop-6.0
             *    
             */
                HubexMain mainPortal = new HubexMain(user);
                mainPortal.ShowDialog();

                //closes window upon user closing the main portal (new window)
                this.Close();
            }
        }
    }
}
