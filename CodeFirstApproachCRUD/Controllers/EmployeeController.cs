using CodeFirstApproachCRUD.Exceptions;
using CodeFirstApproachCRUD.Models;
using CodeFirstApproachCRUD.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproachCRUD.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var empData = await _employeeRepository.GetEmployeesAsync();
                return View(empData);
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException("Error retrieving employee data.", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                await PopulateDepartmentsDropDownList();
                return View();
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException("Error loading add page.", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _employeeRepository.AddAsync(employee);
                    TempData["insert_success"] = "Data Inserted...";
                    return RedirectToAction(nameof(Index));
                }
                await PopulateDepartmentsDropDownList(employee.DeptId);
                return View(employee);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("IX_Employees_Email") ?? false)
                {
                    TempData["ErrorMessage"] = "Email address already exists.";
                    await PopulateDepartmentsDropDownList(employee.DeptId);
                    return View(employee);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException("Error adding employee.", ex);
            }
        }

        private async Task PopulateDepartmentsDropDownList(object? selectedDepartment = null)
        {
            try
            {
                var departmentsQuery = await _departmentRepository.GetDepartmentsAsync();
                var orderedDepartments = departmentsQuery.OrderBy(d => d.DeptName);
                ViewBag.DeptId = new SelectList(orderedDepartments, "DeptId", "DeptName", selectedDepartment);
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException("Error populating department dropdown list.", ex);
            }
        }


        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException("Error retrieving employee details.", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                await PopulateDepartmentsDropDownList(employee.DeptId);
                return View(employee);
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException("Error loading edit page.", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _employeeRepository.UpdateAsync(employee);
                    TempData["update_success"] = "Updated...";
                    return RedirectToAction(nameof(Index));
                }
                await PopulateDepartmentsDropDownList(employee.DeptId);
                return View(employee);
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException("Error editing employee.", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException("Error loading delete page.", ex);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _employeeRepository.DeleteAsync(id);
                TempData["delete_success"] = "Deleted...";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException("Error deleting employee.", ex);
            }
        }
    }
}
