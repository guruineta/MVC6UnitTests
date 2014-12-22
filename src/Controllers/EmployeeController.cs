using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Models;

namespace Controllers
{
	public class EmployeeController : Controller
	{
		private EmployeeContext db = new EmployeeContext();
		private IEmployeeRepository _employeeRepository;

		public EmployeeController(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		// GET: Employee
        public IActionResult Index()
		{
			return View(_employeeRepository.FindAll().ToList());
		}

		// GET: Employee/Details/5
        public IActionResult Details(System.Int32? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(404);
			}

			Employee employee = _employeeRepository.Get(id);
			if (employee == null)
			{
				return new HttpStatusCodeResult(404);
			}

			return View(employee);
		}

		// GET: Employee/Create
        public IActionResult Create()
		{
			return View();
		}

		// POST: Employee/Create
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Employee employee)
		{
			if (ModelState.IsValid)
			{
				_employeeRepository.Save(employee);
				return RedirectToAction("Index");
			}

			return View("Create", employee);
		}

		// GET: Employee/Edit/5
        public IActionResult Edit(System.Int32? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(404);
			}

			Employee employee = _employeeRepository.Get(id);
			if (employee == null)
			{
				return new HttpStatusCodeResult(404);
			}

			return View(employee);
		}

		// POST: Employee/Edit/5
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Employee employee)
		{
			if (ModelState.IsValid)
			{
				_employeeRepository.Update(employee);
				return RedirectToAction("Index");
			}

			return View(employee);
		}

		// GET: Employee/Delete/5
        [ActionName("Delete")]
		public IActionResult Delete(System.Int32? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(404);
			}

			Employee employee = _employeeRepository.Get(id);
			if (employee == null)
			{
				return new HttpStatusCodeResult(404);
			}

			return View(employee);
		}

		// POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(System.Int32 id)
		{
			Employee employee = _employeeRepository.Get(id);
			_employeeRepository.Delete(employee);

            return RedirectToAction("Index");
        }
    }
}
