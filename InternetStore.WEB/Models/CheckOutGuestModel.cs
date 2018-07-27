using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetStore.WEB.Models
{
    public class CheckOutGuestModel
    {
        [MinLength(2, ErrorMessage = "Введите корректное имя")]
        [MaxLength(40, ErrorMessage = "Слишком длинное имя")]
        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MinLength(2, ErrorMessage = "Введите корректную фамилию")]
        [MaxLength(40, ErrorMessage = "Слишком длинная фамилия")]
        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Введите действительный адрес электронной почты")]
        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [RegularExpression(@"^(8|\+7)?[\- ]?\(?\d{3}\)?[\- ]?\d{3}([\- ]?[\d\- ]{2}){2}$", ErrorMessage = "Введите корректный номер телефона")]
        [Required(ErrorMessage = "Введите телефон")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [MinLength(2, ErrorMessage = "Введите корректный адрес")]
        [MaxLength(40, ErrorMessage = "Слишком длинный адрес")]
        [Required(ErrorMessage = "Введите адрес")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [RegularExpression(@"^[0-9]{6}?$", ErrorMessage = "Некорректный почтовый индекс")]
        [Required(ErrorMessage = "Введите почтовый индекс")]
        [Display(Name = "Postcode / zip")]
        public string ZipCode { get; set; }

        [Display(Name = "Create an account?")]
        public bool CreateAccount { get; set; }

        public RegistrationModel RegModel { get; set; }
    }
}