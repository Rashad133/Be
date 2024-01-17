using System.ComponentModel.DataAnnotations;

namespace Be.Areas.ViewModels
{
	public class RegisterVM
	{
		[Required]
		[MinLength(4)]
		[MaxLength(30)]
		public string UserName { get; set; }
		[Required]
		[MinLength(3)]
		[MaxLength(30)]
		public string Name { get; set; }
		[Required]
		[MinLength(3)]
		[MaxLength(30)]
		public string Surname { get; set; }
		
		[DataType(DataType.EmailAddress)]
		[Required(ErrorMessage ="Please enter correct email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Is required")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set;}

	}
}
