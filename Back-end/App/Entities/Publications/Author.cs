using System;
using System.Text.RegularExpressions;


namespace App.Entities
{
    public class Author : FormEntity
    {
        
        private static readonly string _nameMask = "^[А-ЯЁ][а-яё]* [А-ЯЁ][а-яё]* ([А-ЯЁ][а-яё]*)?$";
        private const string _nameSchema = "Surname Firstname Patronymic";

        public override string Name
        {
            set
            {
                if (Regex.IsMatch(value, _nameMask) == false)
                    throw new ArgumentException();
                base.Name = value;
            }
            get
            {
                return base.Name;
            }
        }

        private string[] FIO => Name.Split(' ');
        public string FirstName => FIO[0];
        public string Surname => FIO[1];
        public string Patronymic => FIO[2];
    }
}
