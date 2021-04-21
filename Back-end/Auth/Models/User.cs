using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Various;

namespace Auth.Models
{
    public class User : IdentityUser
    {
        private const int _minAge = 12;
        private const int _maxAge = 80;

        private static readonly string _minAgeMessage;
        private static readonly string _maxAgeMessage;

        private int _age;

        static User()
        {
            _minAgeMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Less, nameof(Age), _minAge.ToString());
            _maxAgeMessage = ExceptionMessages.GetMessage(ExceptionMessageType.More, nameof(Age), _maxAge.ToString());
        }

        public int Age
        {
            set
            {
                if (value < _minAge)
                    throw new ArgumentException(_minAgeMessage);
                if (value > _maxAge)
                    throw new ArgumentException(_maxAgeMessage);
                _age = value;
            }
            get
            {
                return _age;
            }
        }
    }
}
