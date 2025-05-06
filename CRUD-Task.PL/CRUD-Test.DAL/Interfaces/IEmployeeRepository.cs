using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_Test.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Test.DAL.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();

        Task<List<Employee>> GetEmployeesAsync(int pageNumber, int pageSize);

        Task<List<Employee>> SearchEmployeeAsync(string name);

        Task<Employee?> GetByIdAsync(int id);

        void Add(Employee employee);

        void Delete(Employee employee);

        void Update(Employee employee);
    }
}
