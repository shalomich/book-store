using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Various;

namespace App.Entities
{
    public class Author : Entity
    {
        private static readonly DateTime _maxBirthDate;
        private static DateTime _maxDeathDate => DateTime.Today;
        
        private static readonly string _nameMask = "^[А-ЯЁ][а-яё]* [А-ЯЁ][а-яё]* [А-ЯЁ][а-яё]*$";
        private const string _nameSchema = "Surname Firstname Patronymic";

        private static readonly string _maxBirthDateMessage;
        private static readonly string _maxDeathDateMessage;
        private static readonly string _invalidNameMessage;

                                                                                  
        private DateTime _birthDate;
        private DateTime? _deathDate;

        static Author()
        {
            var majorityYear = 18;
            var today = DateTime.Today;
            _maxBirthDate = new DateTime(today.Year - majorityYear, today.Month, today.Day);

            _maxBirthDateMessage = ExceptionMessages.GetMessage(ExceptionMessageType.More, "BirthDate", _maxBirthDate.ToString());
            _maxDeathDateMessage = ExceptionMessages.GetMessage(ExceptionMessageType.More, "DeathDate", _maxDeathDate.ToString());
            _invalidNameMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Invalid, "Name", _nameSchema);
        }

        public string Biography { set; get; }

        public DateTime? DeathDate 
        {
            set 
            {
                if (value > _maxDeathDate)
                    throw new ArgumentOutOfRangeException(_maxDeathDateMessage);
                _deathDate = value;
            }
            get 
            {
                return _deathDate;
            } 
        }

        public DateTime BirthDate
        {
            set
            {
                if (value > _maxBirthDate)
                    throw new ArgumentOutOfRangeException(_maxBirthDateMessage);
                _birthDate = value;
            }
            get
            {
                return _birthDate;
            }
        }

       
        public override string Name
        {
            set
            {
                if (Regex.IsMatch(value, _nameMask) == false)
                    throw new ArgumentException(_invalidNameMessage);
                base.Name = value;
            }
            get
            {
                return base.Name;
            }
        }
        public List<Publication> Publications { set; get; }
    }
}
