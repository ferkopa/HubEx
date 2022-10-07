/**
 * @author:  Paulina Ferko (student num. 991476228)
 * @date:    August 8th, 2022
 * File: HubexMain.xaml.cs
 * Solution Name: AS2_FerkoS2022
 * 
 * @details: WPF code-behind to handle controls for HubexMain.xaml window.
 *           Code handles all button controls and combobox filtration from the
 *           main portal page upon logging in.
 *           
 * @reference:
 *      Microsoft Docs. (2021). How to open a window - WPF .NET [Source Code].
 *          https://docs.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-window-dialog-box?view=netdesktop-6.0
 */
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AS2_FerkoS2022;

namespace AS2_FerkoS2022.xamlpages
{
    /// <summary>
    ///     Interaction logic for HubexMain.xaml
    /// </summary>
    public partial class HubexMain : Window
    {
        //List to hold employee names once retrieved from database
        List<string> empNames = new List<string>();

        /// <summary>
        ///     Constructor to display the window
        /// </summary>
        public HubexMain()
        {
            InitializeComponent();
            
        }
        /// <summary>
        ///     Constructor with parameters to display window, and loads the user's
        ///     name from main login page. Also loads all employee names into the comboboxes
        ///     on loading
        /// </summary>
        /// <param name="user"></param>
        public HubexMain(User user)
        {
            InitializeComponent();
            
            //dislpays username in welcome message
            usernameDisplay(user);

            //loads employee names into comboxes on load
            employeeFilter();
        }

        /// <summary>
        ///     Method to display username retrieved from login mage
        /// </summary>
        /// <param name="user"></param>
        private void usernameDisplay(User user)
        {
            //declares string to hold username from the User class
            string username = user.Username;

            //displays welcome message on screen
            lblUsername.Content = $"Welcome, {username}";

        }
        /// <summary>
        ///     Method to generate new instance of window to get all employees
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetEmployees_Click(object sender, RoutedEventArgs e)
        {
            /**
             * Creating new instance of window and displaying it as a new window on screen
             * 
             * Code Reference:
             *    Title: How to open a window - WPF .NET
             *    Author: Microsoft Docs
             *    Date: 2021
             *    Code version: N/A
             *    Availability:  https://docs.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-window-dialog-box?view=netdesktop-6.0
             *    
            */
            AllNorthwindEmployees getEmployeeList = new AllNorthwindEmployees();
            getEmployeeList.ShowDialog();
        }
        /// <summary>
        ///     Method to generate new window when searching for employee by name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmployeeByName_Click(object sender, RoutedEventArgs e)
        {
            /**
             * Creating new instance of window and displaying it as a new window on screen
             * 
             * Code Reference:
             *    Title: How to open a window - WPF .NET
             *    Author: Microsoft Docs
             *    Date: 2021
             *    Code version: N/A
             *    Availability:  https://docs.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-window-dialog-box?view=netdesktop-6.0
             *    
            */
            EmployeeSearch employeeSearch = new EmployeeSearch();
            employeeSearch.ShowDialog();

        }

        /// <summary>
        ///     Method to display all orders in a separate window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetOrders_Click(object sender, RoutedEventArgs e)
        {
            /**
             * Creating new instance of window and displaying it as a new window on screen
             * 
             * Code Reference:
             *    Title: How to open a window - WPF .NET
             *    Author: Microsoft Docs
             *    Date: 2021
             *    Code version: N/A
             *    Availability:  https://docs.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-window-dialog-box?view=netdesktop-6.0
             *    
            */
            OrderRetrieval orderRetrieval = new OrderRetrieval();
            orderRetrieval.ShowDialog();
        }

        /// <summary>
        ///     Method to close the application with error handling included
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            //variable to hold message to display in popup messagebox
            string message = "Are you sure that you want to close the application?";
            
            // variable to stor user's choice if they want to exit application
            MessageBoxResult userChoice = MessageBox.Show(message, "Close Application?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //error handling if user chooses to exit application
            if (userChoice.Equals(MessageBoxResult.Yes))
            {
                this.Close();
            }
            
        }

        /// <summary>
        ///     method to load employee names into combobox
        /// </summary>
        private void employeeFilter()
        {
            //database instance
            using (var filter = new HubExNorthwindDivision())
            {
                //LINQ query to find employee names in employee table
                var employeeNames = from emp in filter.Employees
                                    select emp.FirstName + " " + emp.LastName;

                //adds each employee to list collection
                foreach(var row in employeeNames)
                {
                    //adds to collection
                    empNames.Add(row);
                }
            }
            //loads employee names into both comboboxes
            cbxFilterEmployees.ItemsSource = empNames;
            cbxFilterOrders.ItemsSource = empNames;
        }
        
        /// <summary>
        ///     Method to display new window if filtering by employee name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterEmp_click(object sender, RoutedEventArgs e)
        {
            //gets the selected name from combobox and converts to string
            string empName = cbxFilterEmployees.SelectedItem.ToString();

            /**
             * Creating new instance of window and displaying it as a new window on screen
             * Sends the employee name as parameter to display only rsults of that employee
             * 
             * Code Reference:
             *    Title: How to open a window - WPF .NET
             *    Author: Microsoft Docs
             *    Date: 2021
             *    Code version: N/A
             *    Availability:  https://docs.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-window-dialog-box?view=netdesktop-6.0
             *    
            */
            EmployeeFilter empFilter = new EmployeeFilter(empName);
            empFilter.ShowDialog();
           
        }
        /// <summary>
        ///     Method ot display new window if filtering orders by employee name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterOrder_click(object sender, RoutedEventArgs e)
        {
            //gets the selected name from combobox and converts to string
            string empName = cbxFilterOrders.SelectedItem.ToString();

            /**
             * Creating new instance of window and displaying it as a new window on screen
             * Sends the employee name as parameter to display only rsults of that employee
             * 
             * Code Reference:
             *    Title: How to open a window - WPF .NET
             *    Author: Microsoft Docs
             *    Date: 2021
             *    Code version: N/A
             *    Availability:  https://docs.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-window-dialog-box?view=netdesktop-6.0
             *    
            */
            OrderSearch ordFilter = new OrderSearch(empName);
            ordFilter.ShowDialog();
        }
    }
}
