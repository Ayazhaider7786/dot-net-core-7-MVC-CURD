using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using modelToDb.Data;
using modelToDb.Models;
using modelToDb.Models.Domain;

namespace modelToDb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly demoDbContext demoDb;

        public EmployeeController(demoDbContext demoDb)
        {
            this.demoDb = demoDb;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new EmployeeModel
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth

            };
            await demoDb.Employees.AddAsync(employee);
            await demoDb.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task <IActionResult> Index() {

            var employee = await  demoDb.Employees.ToListAsync();


            return View(employee);
        
        }
        [HttpGet]
        public async Task< IActionResult> View (Guid id)
        {
            var employee = await demoDb.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var employeeModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth

                };
                return await Task.Run( () => View("View", employeeModel));
            }
            else {
                return RedirectToAction("Index");
            }

            
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel newModel) {
            var employee = await demoDb.Employees.FindAsync(newModel.Id);

            if (employee != null)
            {
                employee.Name= newModel.Name;
                employee.Email=newModel.Email;
                employee.Salary=newModel.Salary;
                employee.Department=newModel.Department;
                employee.DateOfBirth=newModel.DateOfBirth;

                await demoDb.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            // send user to error page saying Data wasnot found
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel newModel) {
            var employee = await demoDb.Employees.FindAsync(newModel.Id);

            if (employee != null)
            {
                demoDb.Employees.Remove(employee);
                await demoDb.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
