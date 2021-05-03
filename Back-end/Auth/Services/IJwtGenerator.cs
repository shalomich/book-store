using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public interface IJwtGenerator
    {
        string CreateToken(User user, string role);
    }
}
