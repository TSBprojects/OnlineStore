using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetStore.WEB.Models
{
    public class RegistrationModel
    {
        [MinLength(6, ErrorMessage = "Пароль должен содержать мимнимум 6 символов")]
        [MaxLength(20, ErrorMessage = "Пароль должен содержать максмимум 20 символов")]
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        public string ConfirmPassword { get; set; }
    }
}