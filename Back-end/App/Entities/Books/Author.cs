using System;
using System.Text.RegularExpressions;


namespace App.Entities
{
    public class Author : RelatedEntity
    {
        
        public const string NameMask = "^[А-ЯЁ][а-яё]* [А-ЯЁ][а-яё]*( [А-ЯЁ][а-яё]*)?$";
        public const string NameSchema = "Surname Firstname Patronymic?";

        private readonly string WrongNameMessage = $"Name must has template {NameSchema}";

        public override string Name
        {
            set
            {
                if (Regex.IsMatch(value, NameMask) == false)
                    throw new ArgumentException(WrongNameMessage);
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
