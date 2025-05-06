using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_Task.BLL.DTOs;
using CRUD_Test.DAL;
using CRUD_Test.DAL.Entities;

namespace CRUD_Task.BLL.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task AddAsync(EmployeeAddDto employeeAddDto)
        {
            if(employeeAddDto is not null)
            {

                var employee = new Employee
                {
                    FirstName = employeeAddDto.FirstName,
                    LastName = employeeAddDto.LastName,
                    Email = employeeAddDto.Email,
                    Position = employeeAddDto.Position
                };

                _unitOfWork.EmployeeRepository.Add(employee);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new Exception("error, employee cannot be null");

            }

        }


        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee is null)
            {
                return false;
            }

            _unitOfWork.EmployeeRepository.Delete(employee);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<EmployeeReadDto>> GetAllAsync()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();

            var employeeDto = employees.Select(e => new EmployeeReadDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Position = e.Position
            }).ToList();

            return employeeDto;
        }

        public async Task<EmployeeReadDto?> GetByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                return null;
            }

            return new EmployeeReadDto
            {
                Id = employee.Id,
               FirstName = employee.FirstName,
               LastName = employee.LastName,
               Email = employee.Email,
               Position = employee.Position
            };
        }
        

        public async Task<EmployeeReadDto?> UpdateAsync(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee is null)
            {
                return null;
            }

            employee.FirstName = employeeUpdateDto.FirstName;
            employee.LastName = employeeUpdateDto.LastName;
            employee.Email = employeeUpdateDto.Email;
            employee.Position = employeeUpdateDto.Position;

            _unitOfWork.EmployeeRepository.Update(employee);
            await _unitOfWork.SaveChangesAsync();

            return new EmployeeReadDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName= employee.LastName,
                Email = employee.Email,
                Position = employee.Position
            };
        }

        public async Task<List<EmployeeReadDto>> GetEmployeesAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than zero.");
            }
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");
            }
            var employees = await _unitOfWork.EmployeeRepository.GetEmployeesAsync(pageNumber, pageSize);
            if (employees == null)
            {
                throw new KeyNotFoundException("No employees found.");
            }
            return employees.Select(e => new EmployeeReadDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Position = e.Position
            }).ToList();

        }

        public async Task<List<EmployeeReadDto>> SearchEmployeeAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Name cannot be null or empty.");
            }
            var employees = await _unitOfWork.EmployeeRepository.SearchEmployeeAsync(name);
            if (employees == null)
            {
                throw new KeyNotFoundException($"No employees found with name {name}.");
            }
            return employees.Select(e => new EmployeeReadDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Position = e.Position
            }).ToList();
        }

    }
}
