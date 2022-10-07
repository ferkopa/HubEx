/**
 * @author:  Paulina Ferko (student num. 991476228)
 * @date:    August 8th, 2022
 * File: EmployeeFilter.xaml.cs
 * Solution Name: AS2_S2022_991476228
 * 
 * @details: WPF code-behind to handle controls for EmployeeFilter.xaml window.
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
    ///     Interaction logic for EmployeeFilter.xaml
    /// </summary>
    public partial class EmployeeFilter : Window
    {
        /// <summary>
        ///     Default constructor for Employee Filter class
        /// </summary>
        public EmployeeFilter()
        {
            InitializeComponent();
            
        }
        /// <summary>
        ///     Constructor with parameter to pass selected employee name
        /// </summary>
        /// 
        /// <param name="name"></param>
        public EmployeeFilter(string name)
        {
            //displays page
            InitializeComponent();

            //method to populate datagrid upon loading window
            DisplayFilteredEmployee(name);
        }

        /// <summary>
        ///     Method to populate the datagrid in .xaml window filtering
        ///     based on selected employee
        /// </summary>
        /// <param name="name"></param>
        private void DisplayFilteredEmployee(string name)
        {
            //instance variable based on database object
            using (var filter = new HubExNorthwindDivision())
            {
                //variable to reference employees table from database
                var empTable = filter.Employees;

                //variable to reference territory table from database
                var terTable = filter.EmployeeTerritories;

                /**
                 * LINQ query to join employee table with employee territories table
                 * query joins tables based on employee id, and retrieves the required
                 * employee info to display in data grid. Used specifically for filtering
                 * data for each employee
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
                        terTable,
                        filter1 => filter1.EmployeeID,
                        filter2 => filter2.EmployeeID,
                        (filter1, filter2) => new
                        {
                            EmployeeID = filter1.EmployeeID,
                            EmployeeFirstName = filter1.FirstName,
                            EmployeeLastName = filter1.LastName,
                            EmployeeTitle = filter1.Title,
                            EmployeeBirthDate = DbFunctions.TruncateTime(filter1.BirthDate),
                            EmployeeTerritoryId = filter2.TerritoryID

                        }
                    ).Where(x => x.EmployeeFirstName + " " + x.EmployeeLastName == name);

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
                dgEmployeeFilter.ItemsSource = newDisplay.ToList();
            }
        }

    }
}
