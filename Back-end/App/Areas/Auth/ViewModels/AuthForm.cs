using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Auth.ViewModels
{
    public record AuthForm
    {
        [NotNull]
        [Email]
        public string Email { init; get; }

        [NotNull]
        public string Password { init; get; }

        public void Deconstruct(out string email, out string password)
        {
            email = Email;
            password = Password;
        }
    }
}
