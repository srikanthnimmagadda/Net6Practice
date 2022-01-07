using Books.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Web.Data;
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
}
