﻿/**
 * @author:  Paulina Ferko (student num. 991476228)
 * @date:    August 8th, 2022
 * File: OrderSearch.xaml.cs
 * Solution Name: AS2_S2022_991476228
 * 
 * @details: WPF code-behind to handle controls for OrderSearch.xaml window.
 *           Code handles populating employee names into a combobox, and uses LINQ
 *           to retreive employee info from a database. Code referenced includes LINQ query
 *           and DataGrid population (references listed below).
 *           
 * @reference:
 *      GeeksForGeeks. (2019). LINQ | Join (Inner Join) [Source code].
 *          https://www.geeksforgeeks.org/linq-join-inner-join/#:%7E:text=In%20LINQ%2C%20an%20inner%20join,in%20the%20result%20data%20set
 *      
 *      Microsoft Docs. (n.d.). DataGrid Class (System.Windows.Controls) [Source Code].
 *          https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.datagrid?view=windowsdesktop-6.0
 *      
 *      Microsoft Docs. (n.d.). DbFunctions.TruncateTime Method (System.Data.Entity) [Source Code]. https://docs.microsoft.com/en-us/dotnet/api/system.data.entity.dbfunctions.truncatetime?view=entity-framework-6.2.0
 *      
 *      Microsoft Docs. (n.d.). Enumerable.Where Method (System.Linq) [Source Code].
 *          https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-6.0
 */
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace AS2_FerkoS2022.xamlpages
{
    /// <summary>
    ///     Interaction logic for OrderSearch.xaml
    /// </summary>
    public partial class OrderSearch : Window
    {
        /// <summary>
        ///     Default constructor to initialize window
        /// </summary>
        public OrderSearch()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Constructor with parameters to launch window and retrieve employee name from main portal
        /// </summary>
        /// <param name="name"></param>
        public OrderSearch(string name)
        {
            InitializeComponent();

            //method to retrieve name from main portal and display data in datagrid
            DisplayFilteredOrders(name);
        }

        /// <summary>
        ///     Method to display data in datagrid based on employee name filtration
        /// </summary>
        /// <param name="name"></param>
        private void DisplayFilteredOrders(string name)
        {
            //database4 instance
            using (var filter = new HubExNorthwindDivision())
            {
                //variable to reference employees table from database
                var empTable = filter.Employees;

                //variable to reference orders table from database
                var ordTable = filter.Orders;

                /**
                 * LINQ query to join employee table with orders table
                 * query joins tables based on employee id, and retrieves the required
                 * employee info to display in data grid. Used specifically for filtering
                 * data for each order based on which employee placed the order
                 * 
                 * Code Reference:
                 *    Title: LINQ | Join (Inner Join)
                 *    Author: GeeksForGeeks
                 *    Date: 2019
                 *    Code version: N/A
                 *    Availability: https://www.geeksforgeeks.org/linq-join-inner-join/#:%7E:text=In%20LINQ%2C%20an%20inner%20join,in%20the%20result%20data%20set
                 *   
                 
                 * Code Reference:
                 *    Title: Enumerable.Where Method (System.Linq)
                 *    Author: Microsoft Docs
                 *    Date: n.d.
                 *    Code version: N/A
                 *    Availability:  https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-6.0
                 * 
                 * Code Reference:
                 *    Title: DbFunctions.TruncateTime Method (System.Data.Entity)
                 *    Author:Microsoft Docs
                 *    Date: n.d.
                 *    Code version: N/A
                 *    Availability: https://docs.microsoft.com/en-us/dotnet/api/system.data.entity.dbfunctions.truncatetime?view=entity-framework-6.2.0
                 *  
                 */
                var newDisplay = empTable.Join(
                        ordTable,
                        filter1 => filter1.EmployeeID,
                        filter2 => filter2.EmployeeID,
                        (filter1, filter2) => new
                        {
                            OrderID = filter2.OrderID,
                            EmployeeName = filter1.FirstName + " " + filter1.LastName,
                            OrderedDate = DbFunctions.TruncateTime(filter2.OrderDate),
                            ShipDate = DbFunctions.TruncateTime(filter2.ShippedDate),
                            ShipCity = filter2.ShipCity,
                            ShipCountry = filter2.ShipCountry
                        }
                    ).Where(x => x.EmployeeName == name);

                /**
                 * Displaying information from the query to datagrid after being converted to list format
                 * 
                 * Code reference:
                 *    Title: DataGrid Class (System.Windows.Controls)
                 *    Author: Microsoft Docs
                 *    Date: n.d.
                 *    Code version: N/A
                 *    Availability: https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.datagrid?view=windowsdesktop-6.0
                 */
                dgOrderFilter.ItemsSource = newDisplay.ToList();
            }
        }
    }
}
