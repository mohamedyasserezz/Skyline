using Microsoft.EntityFrameworkCore;
using Skyline.Models;

namespace Skyline.DbContexts
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Department> Departments { get; set; }
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}
	}
}
