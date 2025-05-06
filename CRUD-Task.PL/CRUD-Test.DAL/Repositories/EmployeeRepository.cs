using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_Test.DAL.Context;
using CRUD_Test.DAL.Entities;
using CRUD_Test.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Test.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Employee employee)
        {
            _context.Set<Employee>().Add(employee);
        }

        public void Delete(Employee employee)
        {
            _context.Remove(employee);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Set<Employee>().AsNoTracking().ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Set<Employee>().FindAsync(id);
        }

        public void Update(Employee employee)
        {
            
        }

        public async Task<List<Employee>> SearchEmployeeAsync(string name)
        {
            return await _context.Employees
                                 .Where(e => e.FirstName.ToLower().Contains(name.ToLower()) || e.LastName.ToLower().Contains(name.ToLower()))
                                 .ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesAsync(int pageNumber, int pageSize)
        {
            return await _context.Employees
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();
        }
    }
}
