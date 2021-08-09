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
        private const int _minPageQuantity = 1;
        private const int _maxPageQuantity = int.MaxValue;

        private const string _IsbnMask = @"^978-5-\d{6}-\d{2}-\d{1}$";
        private const string _formatMask = @"^[1-9]\d{1}x[1-9]\d{1}x[1-9]$";

        private const string _IsbnSchema = "978-5-XXXXXX-XX-X";       
        private const string _formatSchema = "(10-99)x(10-99)x(1-9)";

        private static readonly string _notExistGenreMessage;
        private static readonly string _notExistCoverArtMessage;
        private static readonly string _notExistAgeLimitMessage;
        private static readonly string _minReleaseYearMessage;
        private static readonly string _maxReleaseYearMessage;
        private static readonly string _minPageQuantityMessage;
        private static readonly string _maxPageQuantityMessage;
        private static readonly string _invalidIsbnMessage;
        private static readonly string _invalidFormatMessage;

        private int _releaseYear;
        private string _Isbn;
        private ISet<string> _genres;
        private int? _pageQuantity;
        private string _coverArt;
        private string _ageLimit;
        private string _format;

        static Publication()
        {
            _notExistGenreMessage = ExceptionMessages.GetMessage(ExceptionMessageType.NotExist, "Genre", String.Join(" ", GenreConsts));
            _notExistCoverArtMessage = ExceptionMessages.GetMessage(ExceptionMessageType.NotExist, "CoverArt", String.Join(" ", CoverArtConsts));
            _notExistAgeLimitMessage = ExceptionMessages.GetMessage(ExceptionMessageType.NotExist, "AgeLimit", String.Join(" ", AgeLimitConsts));

            _invalidIsbnMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Invalid, "Isbn", _IsbnSchema);
            _invalidFormatMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Invalid, "Format", _formatSchema);

            _maxReleaseYearMessage = ExceptionMessages.GetMessage(ExceptionMessageType.More, "ReleaseYear", _maxReleaseYear.ToString());
            _maxPageQuantityMessage = ExceptionMessages.GetMessage(ExceptionMessageType.More, "PageQuantity", _maxPageQuantity.ToString());

            _minReleaseYearMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Less, "ReleaseYear", _minReleaseYear.ToString());
            _minPageQuantityMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Less, "PageQuantity", _minPageQuantity.ToString());
        }

        public static readonly string[] GenreConsts = 
        {
            "Драма",
            "Ужасы",
            "Научная фантастика",
            "Наука",
            "Боевик",
            "Детектив",
            "Фэнтези"
        };

        public static readonly string[] CoverArtConsts = 
        {
            "Твёрдая",
            "Мягкая"
        };

        public static readonly string[] AgeLimitConsts = 
        {
            "0+",
            "6+",
            "12+",
            "18+"
        };

        public string Type { set; get; }
        public string CoverArt
        {
            set
            {
                if (value != null)
                    if (CoverArtConsts.Contains(value) == false)
                        throw new ArgumentOutOfRangeException(_notExistCoverArtMessage);
                _coverArt = value;
            }

            get
            {
                return _coverArt;
            }
        }
        
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

        public string AgeLimit
        {
            set
            {
                if (AgeLimitConsts.Contains(value) == false)
                    throw new ArgumentOutOfRangeException(_notExistAgeLimitMessage);
                _ageLimit = value;
            }
            get
            {
                return _ageLimit;
            }
        }

        public int? PageQuantity
        {
            set
            {
                if (value < _minPageQuantity)
                    throw new ArgumentOutOfRangeException(_minPageQuantityMessage);
                if (value > _maxPageQuantity)
                    throw new ArgumentOutOfRangeException(_maxPageQuantityMessage);
                _pageQuantity = value;
            }
            get
            {
                return _pageQuantity;
            }
        }
        public ISet<string> Genres
        {
            set
            {
                if (value != null)
                    foreach (var genre in value)
                        if (GenreConsts.Contains(genre) == false)
                            throw new ArgumentOutOfRangeException(_notExistGenreMessage);
                _genres = value;
            }
            get
            {
                return _genres;
            }
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
        public string OriginalName { set; get; }


    }
}
