using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Skyline.Models;
using Skyline.DbContexts;

namespace Skyline.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;



		public EmployeesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;

		}


		//    List<Employee> employees = new List<Employee>()
		//{
		//    new Employee(){
		//            Id = 1001,
		//            FullName = "Abdelrahman Sayed ",
		//            NationalId = "11022033044",
		//            Position = "Pen Tester",
		//            Salary = 9000m
		//        },
		//    new Employee(){
		//            Id = 1002,
		//            FullName = "Wael Mahmoud ",
		//            NationalId = "100220030044",
		//            Position = "Developer",
		//            Salary = 14500m
		//        },
		//    new Employee(){
		//            Id = 1003,
		//            FullName = "Bahaa Ahmed ",
		//            NationalId = "10022055010",
		//            Position = "Team Lead",
		//            Salary = 19500m
		//        },

		//};
		//public IActionResult GetIndexView()
		//{
		//	ViewBag.EmpCountries = new List<string>() { "Egypt", "Sudan", "Kuwait", "Oman" };

		//	return View("Index", _context.Employees.ToList());
		//}

		//public IActionResult Index() { return View(); }


		[HttpGet]
		public IActionResult GetIndexView(string? search)
		{
			ViewBag.EmpCountries = new List<string>() { "Egypt", "Sudan", "Kuwait", "Oman" };

			ViewBag.SearchText = search;

			IQueryable<Employee> QueryableEmps = _context.Employees.AsQueryable();

			if (string.IsNullOrEmpty(search) == false)
			{
				QueryableEmps = QueryableEmps.Where(emp => emp.FullName.Contains(search) || emp.Position.Contains(search));

			}



			return View("Index", QueryableEmps.ToList());
		}








		public IActionResult GetDetailsView(int id)

		{
			//Employee employee = _context.Employees.FirstOrDefault(e=>e.Id==id);
			Employee employee = _context.Employees.Include(emp => emp.Department).FirstOrDefault(emp => emp.Id == id);
			if (employee == null)
			{
				return NotFound();
			}
			else
			{
				return View("Details", employee);
			}

		}
		public string GreatVisitor()
		{
			return "Welcome to Skyline";
		}
		public string CalculateAge(string name, int birthYear)
		{
			return $"Hi,{name}. You are {DateTime.Now.Year - birthYear} years old";
		}

		[HttpGet]
		public IActionResult GetCreateView()
		{
			ViewBag.AllDepartments = _context.Departments.ToList();

			return View("Create");
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		//public IActionResult AddNew(Employee employee)
		//{
		//	if (((employee.HiringDateTime - employee.BirthDate).Days / 365) < 18)
		//	{
		//		ModelState.AddModelError(string.Empty, "Not allowed hiring age (under 18 years)");

		//	}
		//	if (ModelState.IsValid == true)
		//	{


		//		_context.Employees.Add(employee);
		//		_context.SaveChanges();
		//		return RedirectToAction("GetIndexView");
		//	}
		//	else
		//	{
		//		ViewBag.AllDepartments = _context.Departments.ToList();
		//		return View("Create");
		//	}
		//}
		public IActionResult AddNew(Employee employee)
		{
			if (((employee.HiringDateTime - employee.BirthDate).Days / 365) < 18)
			{
				ModelState.AddModelError(string.Empty, "Not allowed hiring age (under 18 years)");
			}

			if (ModelState.IsValid == true)
			{
				if (employee.ImageFile == null)
				{
					employee.ImagePath = "\\images\\No_Image.png";
				}
				else
				{
					// Guid-> Globally Uniqe Identifier  not repeat over the worled
					Guid imageGuid = Guid.NewGuid();
					string imageExtension = Path.GetExtension(employee.ImageFile.FileName);
					employee.ImagePath = "\\images\\" + imageGuid + imageExtension;

					string imageUploadPath = _webHostEnvironment.WebRootPath + employee.ImagePath;

					FileStream imageStream = new FileStream(imageUploadPath, FileMode.Create);
					employee.ImageFile.CopyTo(imageStream);
					imageStream.Dispose();
				}

				_context.Employees.Add(employee);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
				ViewBag.AllDepartments = _context.Departments.ToList();
				return View("Create");
			}
		}





		[HttpGet]
		public IActionResult GetEditView(int id)
		{
			//Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
			Employee employee = _context.Employees.Find(id);
			if (employee == null)
			{
				return NotFound();
			}
			else
			{
				ViewBag.AllDepartments = _context.Departments.ToList();
				return View("Edit", employee);
			}
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult EditCurrent(Employee employee)
		{
			if (((employee.HiringDateTime - employee.BirthDate).Days / 365) < 18)
			{
				ModelState.AddModelError(string.Empty, "Not allowed hiring age (under 18 years)");

			}
			if (ModelState.IsValid == true)
			{
				if (employee.ImageFile != null)
				{
					if (employee.ImagePath != "\\images\\No_Image.png")
					{
						System.IO.File.Delete(_webHostEnvironment.WebRootPath + employee.ImagePath);
					}

					// Guid-> Globally Uniqe Identifier  not repeat over the worled
					Guid imageGuid = Guid.NewGuid();
					string imageExtension = Path.GetExtension(employee.ImageFile.FileName);
					employee.ImagePath = "\\images\\" + imageGuid + imageExtension;

					string imageUploadPath = _webHostEnvironment.WebRootPath + employee.ImagePath;

					FileStream imageStream = new FileStream(imageUploadPath, FileMode.Create);
					employee.ImageFile.CopyTo(imageStream);
					imageStream.Dispose();

				}

				_context.Employees.Update(employee);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
				ViewBag.AllDepartments = _context.Departments.ToList();
				return View("Edit");
			}


		}


		[HttpGet]
		public IActionResult GetDeleteView(int id)
		{
			//Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
			Employee employee = _context.Employees.Find(id);
			if (employee == null)
			{
				return NotFound();
			}
			else
			{
				return View("Delete", employee);
			}
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult DeleteCurrent(int id)
		{
			//Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
			Employee employee = _context.Employees.Find(id);
			if (employee == null)
			{
				return NotFound();
			}
			else
			{
				if (employee.ImagePath != "\\images\\No_Image.png")
				{
					System.IO.File.Delete(_webHostEnvironment.WebRootPath + employee.ImagePath);
				}
				_context.Remove(employee);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
		}

	}
}