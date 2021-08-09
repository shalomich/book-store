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
        
        private static readonly string _nameMask = "^[А-ЯЁ][а-яё]* [А-ЯЁ][а-яё]* [А-ЯЁ][а-яё]*$";
        private const string _nameSchema = "Surname Firstname Patronymic";

        private static readonly string _invalidNameMessage;

        static Author()
        {
            _invalidNameMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Invalid, "Name", _nameSchema);
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
