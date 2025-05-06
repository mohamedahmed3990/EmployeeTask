using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_Task.BLL.DTOs;

namespace CRUD_Task.BLL.Services.EmployeeServices
{
    public interface IEmployeeService 
    {
        Task<List<EmployeeReadDto>> GetAllAsync();

        Task AddAsync(EmployeeAddDto employeeAddDto);

        Task<EmployeeReadDto?> GetByIdAsync(int id);

        Task<EmployeeReadDto?> UpdateAsync( int id, EmployeeUpdateDto doctorUpdateDto);

        Task<bool> DeleteAsync(int id);

        Task<List<EmployeeReadDto>> SearchEmployeeAsync(string name);
        Task<List<EmployeeReadDto>> GetEmployeesAsync(int pageNumber, int pageSize);

    }
}
