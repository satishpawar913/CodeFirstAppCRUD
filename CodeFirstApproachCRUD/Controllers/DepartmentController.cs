using CodeFirstApproachCRUD.Exceptions;
using CodeFirstApproachCRUD.Models;
using CodeFirstApproachCRUD.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstApproachCRUD.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var deptData = await _departmentRepository.GetDepartmentsAsync();
                return View(deptData);
            }
            catch (Exception ex)
            {
                throw new DepartmentNotFoundException("Error retrieving department data.", ex);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw new DepartmentNotFoundException("Error loading create page.", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if department name already exists
                    if (!await _departmentRepository.IsDepartmentNameUnique(department.DeptName))
                    {
                        ModelState.AddModelError("DeptName", "Department name already exists.");
                        return View(department);
                    }

                    await _departmentRepository.AddAsync(department);
                    TempData["insert_success"] = "Data Inserted...";
                    return RedirectToAction(nameof(Index));
                }
                return View(department);
            }
            catch (Exception ex)
            {
                throw new DepartmentNotFoundException("Error creating department.", ex);
            }
        }


        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var department = await _departmentRepository.GetByIdAsync(id);
                if (department == null)
                {
                    return NotFound();
                }
                return View(department);
            }
            catch (Exception ex)
            {
                throw new DepartmentNotFoundException("Error retrieving department details.", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var department = await _departmentRepository.GetByIdAsync(id);
                if (department == null)
                {
                    return NotFound();
                }
                return View(department);
            }
            catch (Exception ex)
            {
                throw new DepartmentNotFoundException("Error loading edit page.", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            try
            {
                if (id != department.DeptId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    await _departmentRepository.UpdateAsync(department);
                    TempData["update_success"] = "Data Updated...";
                    return RedirectToAction(nameof(Index));
                }

                return View(department);
            }
            catch (Exception ex)
            {
                throw new DepartmentNotFoundException("Error editing department.", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var department = await _departmentRepository.GetByIdAsync(id);
                if (department == null)
                {
                    throw new DepartmentNotFoundException($"Department cannot be deleted as it is referenced by employees!");
                }

                await _departmentRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DepartmentNotFoundException ex)
            {
                return RedirectToAction("Error", "Department", new { message = ex.Message });
            }
        }

    }
}
