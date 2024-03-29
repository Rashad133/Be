﻿using System.ComponentModel.DataAnnotations;

namespace Be.Areas.ViewModels
{
	public class LoginVM
	{
		[Required]
		public string UsernameOrEmail { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

	}
}
