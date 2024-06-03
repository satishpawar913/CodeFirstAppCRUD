using CodeFirstApproachCRUD.Models;
using System.Linq.Expressions;

namespace CodeFirstApproachCRUD.Repository
{
    public interface IDepartmentRepository
    {
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(int deptId);
        Task<Department> GetByIdAsync(int deptId);
        Task<IEnumerable<Department>> GetDepartmentsAsync();

        Task<bool> IsDepartmentNameUnique(string departmentName);
    }
}
