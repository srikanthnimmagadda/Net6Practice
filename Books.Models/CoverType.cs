using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
	public class CoverType
	{
		/// <summary>
		/// 
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "Cover Type")]
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
	}
}
