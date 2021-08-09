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
        private const int _minReleaseYear = 2000;
        private static readonly int _maxReleaseYear = DateTime.Today.Year;

        private const string _IsbnMask = @"^978-5-\d{6}-\d{2}-\d{1}$";
        private const string _formatMask = @"^[1-9]\d{1}x[1-9]\d{1}x[1-9]$";

        private const string _IsbnSchema = "978-5-XXXXXX-XX-X";       
        private const string _formatSchema = "(10-99)x(10-99)x(1-9)";

        private static readonly string _minReleaseYearMessage;
        private static readonly string _maxReleaseYearMessage;
        private static readonly string _invalidIsbnMessage;
        private static readonly string _invalidFormatMessage;

        private int _releaseYear;
        private string _Isbn;
        private string _format;
        private int? _pageQuantity;

        static Publication()
        {
            _invalidIsbnMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Invalid, "Isbn", _IsbnSchema);
            _invalidFormatMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Invalid, "Format", _formatSchema);

            _maxReleaseYearMessage = ExceptionMessages.GetMessage(ExceptionMessageType.More, "ReleaseYear", _maxReleaseYear.ToString());
            
            _minReleaseYearMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Less, "ReleaseYear", _minReleaseYear.ToString());
        }

        public string Isbn
        {
            set
            {
                if (Regex.IsMatch(value, _IsbnMask) == false)
                    throw new ArgumentException(_invalidIsbnMessage);
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
                if (value < _minReleaseYear)
                    throw new ArgumentOutOfRangeException(_minReleaseYearMessage);
                if (value > _maxReleaseYear)
                    throw new ArgumentOutOfRangeException(_maxReleaseYearMessage);
                _releaseYear = value;
            }
            get
            {
                return _releaseYear;
            }
        }

        public Publisher Publisher { set; get; }
        public int PublisherId { set; get; }

        public Author Author { set; get; }
        public int AuthorId { set; get; }

        public PublicationType Type { set; get; }
        public int? TypeId { set; get; }

        public ISet<GenrePublication> Genres { set; get; }

        public string OriginalName { set; get; }

        public AgeLimit AgeLimit { set; get; }
        public int? AgeLimitId { set; get; }

        public CoverArt CoverArt { set; get; }
        public int? CoverArtId { set; get; }

        public string PublicationFormat
        {
            set
            {
                if (value != null) 
                    if (Regex.IsMatch(value, _formatMask) == false)
                        throw new ArgumentException(_invalidFormatMessage);
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
