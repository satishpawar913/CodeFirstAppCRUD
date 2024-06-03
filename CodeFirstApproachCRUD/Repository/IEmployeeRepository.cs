using CodeFirstApproachCRUD.Models;

namespace CodeFirstApproachCRUD.Repository
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int employeeId);
        Task<Employee> GetByIdAsync(int employeeId);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}
