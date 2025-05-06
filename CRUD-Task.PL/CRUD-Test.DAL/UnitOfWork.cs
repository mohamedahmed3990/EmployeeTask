using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_Test.DAL.Context;
using CRUD_Test.DAL.Interfaces;

namespace CRUD_Test.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IEmployeeRepository EmployeeRepository { get; }


        public UnitOfWork(AppDbContext context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            EmployeeRepository = employeeRepository;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}
