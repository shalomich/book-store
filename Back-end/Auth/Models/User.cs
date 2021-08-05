using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models
{
    public class User : IdentityUser
    {
        private const int _minAge = 12;
        private const int _maxAge = 80;

        private int _age;
        public int Age
        {
            set
            {
                if (value < _minAge)
                    throw new ArgumentException();
                if (value > _maxAge)
                    throw new ArgumentException();
                _age = value;
            }
            get
            {
                return _age;
            }
        }
    }
}
