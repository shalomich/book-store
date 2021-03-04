using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class Author : Entity
    {
        private static readonly DateTime _maxDeathDate = DateTime.Today;
        private static readonly DateTime _maxBirthDate;
        private static readonly string _nameMask = "^[А-ЯЁ][а-яё]* [А-ЯЁ][а-яё]* [А-ЯЁ][а-яё]*$";

        private static readonly string _maxDeathDateMessage = $"Дата смерти не может быть позже {_maxDeathDate}";
        private static readonly string _maxBirthDateMessage = $"Дата рождения не может быть позже {_maxBirthDate}";
        private static readonly string _invalidNameMessage = "Некорректное значение имени автора:{0}";
                                                                                  
        private string _name;
        private DateTime _birthDate;
        private DateTime? _deathDate;

        static Author()
        {
            var majorityYear = 18;
            var today = DateTime.Today;
            _maxBirthDate = new DateTime(today.Year - majorityYear, today.Month, today.Day);
        } 
        public override string Name 
        { 
            set 
            {
                if (Regex.IsMatch(value, _nameMask) == false)
                    throw new ArgumentException(String.Format(_invalidNameMessage,value));
                _name = value;
            } 
            get 
            {
                return _name;
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

        public string Biography { set; get; }
        public List<Publication> Publications { set; get; }
    }
}
