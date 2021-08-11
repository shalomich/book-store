using App.Entities.Publications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Various;

namespace App.Entities
{
    public class Publication : Product
    {
        public const int MinReleaseYear = 2000;
        public static readonly int MaxReleaseYear = DateTime.Today.Year;

        public const string IsbnTemplate = @"^978-5-\d{6}-\d{2}-\d{1}$";
        public const string FormatTemplate = @"^[1-9]\d{1}x[1-9]\d{1}x[1-9]$";

        private const string IsbnSchema = "978-5-XXXXXX-XX-X";       
        private const string FormatSchema = "(10-99)x(10-99)x(1-9)";

        private static readonly string MinReleaseYearMessage;
        private static readonly string MaxReleaseYearMessage;
        private static readonly string InvalidIsbnMessage;
        private static readonly string InvalidFormatMessage;

        private int _releaseYear;
        private string _Isbn;
        private string _format;
        private int? _pageQuantity;

        static Publication()
        {
            InvalidIsbnMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Invalid, "Isbn", IsbnSchema);
            InvalidFormatMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Invalid, "Format", FormatSchema);

            MaxReleaseYearMessage = ExceptionMessages.GetMessage(ExceptionMessageType.More, "ReleaseYear", MaxReleaseYear.ToString());
            
            MinReleaseYearMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Less, "ReleaseYear", MinReleaseYear.ToString());
        }

        public string Isbn
        {
            set
            {
                if (Regex.IsMatch(value, IsbnTemplate) == false)
                    throw new ArgumentException(InvalidIsbnMessage);
                _Isbn = value;
            }
            get
            {
                return _Isbn;
            }
        }

        public int ReleaseYear
        {
            set
            {
                if (value < MinReleaseYear)
                    throw new ArgumentOutOfRangeException(MinReleaseYearMessage);
                if (value > MaxReleaseYear)
                    throw new ArgumentOutOfRangeException(MaxReleaseYearMessage);
                _releaseYear = value;
            }
            get
            {
                return _releaseYear;
            }
        }

        public virtual Publisher Publisher { set; get; }
        public int PublisherId { set; get; }

        public virtual Author Author { set; get; }
        public int AuthorId { set; get; }

        public virtual PublicationType Type { set; get; }
        public int? TypeId { set; get; }

        public virtual ISet<GenrePublication> Genres { set; get; }

        public string OriginalName { set; get; }

        public virtual AgeLimit AgeLimit { set; get; }
        public int? AgeLimitId { set; get; }

        public virtual CoverArt CoverArt { set; get; }
        public int? CoverArtId { set; get; }

        public string PublicationFormat
        {
            set
            {
                if (value != null) 
                    if (Regex.IsMatch(value, FormatTemplate) == false)
                        throw new ArgumentException(InvalidFormatMessage);
                _format = value;
            }
            get
            {
                return _format;
            }
        }

        public int? PageQuantity { 
            set 
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException();
                _pageQuantity = value;
            }
            get 
            {
                return _pageQuantity;
            } 
        }
      
    }
}
