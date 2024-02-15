using EmployeeManagement.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IMockEmployeeRepository
    {
        private List<Employee> _empList;

        public MockEmployeeRepository()
        {
            //_empList = new List<Employee>()

            //{
            //    new Employee(){Id=1, Name="Mark", Department =Dept.IT, Email="Mark@hotmil.com"},
            //    new Employee(){Id=2, Name="Param", Department=Dept.IT, Email="param@hotmil.com"},
            //    new Employee(){Id=3, Name="Tom", Department= Dept.HR, Email="Tom@hotmil.com"}


            //};
           
        }


        public IEnumerable<Employee> GetAllEmployee()
        {
            var data = File.ReadAllText("EmployeeData.json");
            _empList = JsonSerializer.Deserialize<List<Employee>>(data).OrderBy(x=>x.Id).ToList();

            return _empList;
        }

        public bool UpdateEmployeeListToFile(List<Employee> data)
        {
            var employeeJson = JsonSerializer.Serialize(data);
            File.WriteAllText(@"Employeedata.json", employeeJson);
            return true;
        }

        public Employee GetEmployee(int Id)
        {
            var dataList = GetAllEmployee();
            return dataList.FirstOrDefault(emp => emp.Id == Id);
        }
        public Employee GetEmployeeByNameAndPassword(string Name, string Password)
        {
            var dataList = GetAllEmployee();
            return dataList.FirstOrDefault(emp => emp.Email == Name && emp.Password == Password);
        }
        public Employee Add(Employee employee)
        {
            _empList = GetAllEmployee().ToList();
            employee.Id = _empList.Max(e => e.Id) + 1;
            _empList.Add(employee);
            UpdateEmployeeListToFile(_empList);
            return employee;
        }

        public Employee Update(Employee employeeUpdate)
        {
            var emp = GetEmployee(employeeUpdate.Id);
            if (employeeUpdate != null)
            {
                _empList.Remove(emp);
                emp.Name = employeeUpdate.Name;
                emp.Email = employeeUpdate.Email;
                emp.Department = employeeUpdate.Department;          
            }
            _empList.Add(emp);
            UpdateEmployeeListToFile(_empList);
           return emp;
        }
        public Employee Remove(int Id)
        {
            var EmpRemove = GetEmployee(Id);

            if (EmpRemove !=null)
            {
            _empList.Remove(EmpRemove);
            }
            UpdateEmployeeListToFile(_empList);
            return EmpRemove;
        }

    }
}
