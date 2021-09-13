using System;
using System.Text.RegularExpressions;


namespace App.Entities
{
    public class Author : FormEntity
    {
        
        public const string NameMask = "^[А-ЯЁ][а-яё]* [А-ЯЁ][а-яё]*( [А-ЯЁ][а-яё]*)?$";
        public const string NameSchema = "Surname Firstname Patronymic?";

        public override string Name
        {
            set
            {
                if (Regex.IsMatch(value, NameMask) == false)
                    throw new ArgumentException();
                base.Name = value;
            }
            get
            {
                return base.Name;
            }
        }

        private string[] FIO => Name.Split(' ');

        public string Surname => FIO[0];
        public string FirstName => FIO[1];
        public string Patronymic => FIO[2];
    }
}
