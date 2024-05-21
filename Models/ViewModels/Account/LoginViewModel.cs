﻿using System.ComponentModel.DataAnnotations;

namespace OnlineCourses.Models.ViewModels.Account
{
	public class LoginViewModel
	{
		[Display(Name = "Логин")]
		[Required(ErrorMessage = "Укажите логин")]
		[MaxLength(100, ErrorMessage = "Слишком длинно")]
		[MinLength(3, ErrorMessage = "Логин больше 3 символов")]
		public string? Login { get; set; }

		[Display(Name = "Почта")]
		[Required(ErrorMessage = "Укажите почту")]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }

		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Введите пароль")]
		public string? Password { get; set; }
	}
}
