using Books.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Books.Dal.Data;
public class BooksDbContext : IdentityDbContext
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

    /// <summary>
    /// 
    /// </summary>
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<Company> Companies { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<OrderHeader> OrderHeaders { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<OrderDetail> OrderDetail { get; set; }
}
