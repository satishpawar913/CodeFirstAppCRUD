using CodeFirstApproachCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproachCRUD.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Employee> GetByIdAsync(int employeeId)
        {
            return await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.Include(e => e.Department).ToListAsync();
        }
    }
}
