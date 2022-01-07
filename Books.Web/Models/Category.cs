using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Books.Web.Models;
public class Category
{
    /// <summary>
    /// 
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [DisplayName("Display Order")]
    [Range(1, 100, ErrorMessage = "Display Order must be between 1 and 100 only!!")]
    public int DisplayOrder { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}
