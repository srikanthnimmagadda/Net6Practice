using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Dal.Data;
public class BooksDbContext : DbContext
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="options"></param>
	public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
	{
	}

	/// <summary>
	/// 
	/// </summary>
	public DbSet<Category> Categories { get; set; }

	/// <summary>
	/// 
	/// </summary>
	public DbSet<CoverType> CoverTypes { get; set; }

	/// <summary>
	/// 
	/// </summary>
	public DbSet<Product> Products { get; set; }
}
