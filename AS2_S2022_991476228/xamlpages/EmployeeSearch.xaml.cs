/**
 * @author:  Paulina Ferko (student num. 991476228)
 * @date:    August 8th, 2022
 * File: EmployeeSearch.xaml.cs
 * Solution Name: AS2_S2022_991476228
 * 
 * @details: WPF code-behind to handle controls for EmployeeSearch.xaml window.
 *           Code handles retrieveing info from textboxes and filtering the results 
 *           to find the employee. Code has error handling in case employee is not found in system.
 */
using System;
using System.Linq;
using System.Windows;

namespace AS2_FerkoS2022
{
    /// <summary>
    ///     Interaction logic for EmployeeSearch.xaml
    /// </summary>
    public partial class EmployeeSearch : Window
    {
        /// <summary>
        ///     Constructor to launch window and to give the first textbox focus
        /// </summary>
        public EmployeeSearch()
        {
            InitializeComponent();

            //focusing on firstname textbox upon launching
            txtEmpFirstName.Focus();
        }
        /// <summary>
        ///     Method to control what happens upon clicking button to search for employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_click(object sender, RoutedEventArgs e)
        {
            
            //variable to be assigned a value later
            string empInfo;
            
            //retrieving firstname input from textbox
            string firstname = txtEmpFirstName.Text;

            //retrieving lastname input from textboox
            string lastname = txtEmpLastName.Text;

            //error handling if firstname or lastname are empty or null
            if(firstname.Equals("") || firstname == null || lastname.Equals("") || lastname == null)
            {
                //message to display to user
                string message = "Names cannot be blank. Please enter a valid first and/or lastname";

                //message box popup to show up either textbox is empty/null
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //instance of database 
                using (var searchQuery = new HubExNorthwindDivision())
                {
                    //LINQ query to find employee if first or last name begin with what user has typed
                    var employeeSearch = from emp in searchQuery.Employees
                                         where emp.FirstName.StartsWith(firstname) && emp.LastName.StartsWith(lastname)
                                         select emp;

                    //if there is any instance of the query being true, employee details will be displayed
                   if(employeeSearch.Any())
                    {
                        //storing and converting employee birthdate to a string
                        var dob = employeeSearch.FirstOrDefault().BirthDate.ToString();

                        //storing and converting employee hire date to a string
                        var hireDate = employeeSearch.FirstOrDefault().HireDate.ToString();

                        //stroing query result and the first instance of that query in a variable
                        var details = employeeSearch.FirstOrDefault();

                        //parsing the date of birth to a DateTime object to display only the day, month and year
                        DateTime dateDisplay = DateTime.Parse(dob);

                        //parsing the hire date to a DateTime object to display only the day, month and year
                        
                        DateTime hireDisplay = DateTime.Parse(hireDate);

                        //storing employee details in a single string to display later to user
                        empInfo = $"Employee Name: {details.FirstName} " +
                            $"{details.LastName} \n" +
                            $"Position/Title: {details.Title}\n" +
                            $"Date of Birth: {dateDisplay.ToString("dd-MM-yyyy")}\t\t" +
                            $"Hire Date: {hireDisplay.ToString("dd-MM-yyyy")}\n\n" +
                            $"Contact Info: \n{details.Address} " +
                            $"{details.City} {details.Region} {details.Country}" +
                            $" {details.PostalCode}\n" +
                            $"Phone Number: {details.HomePhone}";
                    }
                    //error handling if the user is not found in the system
                    else
                    {
                        empInfo = firstname + " " + lastname + " is not found in the database";
                    }
                    
                   //displaying the details to user in a label
                    lblDisplay.Content = empInfo;
                }
            }
        }
    }
}
