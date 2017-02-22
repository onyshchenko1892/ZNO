using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIEE.WEB.UI.Models
{
    public class MemberModel
    {
        public class LoginMemberModel
        {
            [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
            [RegularExpression(@"^(([^<>()\[\]\\.,;:\s@']+(\.[^<>()\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Некоректний email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
            [StringLength(32, MinimumLength = 6, ErrorMessage = "Пароль повинен складатися мінімум з 6-и, максимум з 32-ох символів")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public class RegisterMemberModel
        {
            [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
            public string Surname { get; set; }

            [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
            [RegularExpression(@"^(([^<>()\[\]\\.,;:\s@']+(\.[^<>()\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Некоректний email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
            [StringLength(32, MinimumLength = 6, ErrorMessage = "Пароль повинен складатися мінімум з 6-и, максимум з 32-ох символів")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Поле обов'язкове для заповнення")]
            [StringLength(32, MinimumLength = 6, ErrorMessage = "Пароль повинен складатися мінімум з 6-и, максимум з 32-ох символів")]
            [Compare("Password", ErrorMessage = "Паролі не співпадають")]
            [DataType(DataType.Password)]
            public string ConfirmedPassword { get; set; }
        }
    }
}