using CodeFirstApproachCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproachCRUD.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeDbContext _context;

        public DepartmentRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsDepartmentNameUnique(string departmentName)
        {
            return !await _context.Departments.AnyAsync(d => d.DeptName == departmentName);
        }

        public async Task<Department> GetByIdAsync(int deptId)
        {
            return await _context.Departments.Include(d => d.Employees).FirstOrDefaultAsync(d => d.DeptId == deptId);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.Include(d => d.Employees).ToListAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(department.DeptId);
            if (existingDepartment != null)
            {
                _context.Entry(existingDepartment).CurrentValues.SetValues(department);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int deptId)
        {
            var department = await _context.Departments.FindAsync(deptId);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
