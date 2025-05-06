using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_Test.DAL.Interfaces;

namespace CRUD_Test.DAL
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IEmployeeRepository EmployeeRepository { get;}

        Task<int> SaveChangesAsync();
    }
}
