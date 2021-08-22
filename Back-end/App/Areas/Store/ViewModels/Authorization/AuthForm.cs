using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Store.ViewModels.Authorization
{
    public record AuthForm
    {
        [Required]
        [Email]
        public string Email { init; get; }

        [Required]
        public string Password { init; get; }

        public void Deconstruct(out string email, out string password)
        {
            email = Email;
            password = Password;
        }
    }
}
