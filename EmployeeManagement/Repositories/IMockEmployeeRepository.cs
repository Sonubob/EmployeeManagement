using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Repositories
{
    public interface IMockEmployeeRepository
    {
        /// <summary>
        /// Gets all the employees as list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employee> GetAllEmployee();
        /// <summary>
        /// Gets an employee details by employeeId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Employee GetEmployee(int Id);
        /// <summary>
        /// Adds a new employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Employee Add(Employee employee);
        /// <summary>
        /// Updates an existing employee
        /// </summary>
        /// <param name="employeeUpdate"></param>
        /// <returns></returns>
        public Employee Update(Employee employeeUpdate);
        /// <summary>
        /// Removes an employee entry
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Employee Remove(int Id);
        /// <summary>
        /// Get employee by the login details
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Employee GetEmployeeByNameAndPassword(string Name, string Password);
    }
}
