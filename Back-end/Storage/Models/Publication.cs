using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class Publication : Product
    {
        private const int _minReleaseYear = 2000;
        private static readonly int _maxReleaseYear = DateTime.Today.Year;
        private const int _minPageQuantity = 1;
        private const int _maxPageQuantity = int.MaxValue;
        private const string _ISBNMask = "978-5-[0-9]{6}-[0-9]{2}-[0-9]{1}";
        private const string _formatMask = "[0-9]+X[0-9]+X[0-9]+";

        private static readonly string _notExistTypeMessage = "Не существует данного типа публикации {0}";
        private static readonly string _notExistGenreMessage = "Не существует данного жанра {0}";
        private static readonly string _notExistCoverArtMessage = "Не существует данного типа обложки {0}";
        private static readonly string _notExistAgeLimitMessage = "Не существует данного возрастного ограничения {0}";
        private static readonly string _minReleaseYearMessage = $"Год релиза не должен быть меньше {_minReleaseYear}";
        private static readonly string _maxReleaseYearMessage = $"Год релиза не должен быть больше {_maxReleaseYear}";
        private static readonly string _minPageQuantityMessage = $"Количество страниц не должно быть меньше {_minPageQuantity}";
        private static readonly string _maxPageQuantityMessage = $"Количество страниц не должно быть больше {_maxPageQuantity}";
        private static readonly string _invalidISBNMessage = "Некорректное значение ISBN:{0}, должно быть 978-5-XXXXXX-XX-X";
        private static readonly string _invalidFormatMessage = "Некорректное значение формата:{0}, должно быть длинаXширинаXтолщина";

        private int _releaseYear;
        private string _type;
        private string _ISBN;
        private ISet<string> _genres;
        private int? _pageQuantity;
        private string _coverArt;
        private string _ageLimit;
        private string _format;

        public static readonly string[] PublicationTypeConsts =
        {
            "Книга",
            "Манга",
            "Ранобэ",
            "Графический роман",
            "Артбук"
        };
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
            "Твердая",
            "Мягкая"
        };

        public static readonly string[] AgeLimitConsts = 
        {
            "0+",
            "6+",
            "12+",
            "18+"
        };
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

        public string Type
        {
            set
            {
                if (PublicationTypeConsts.Contains(value) == false)
                    throw new ArgumentOutOfRangeException(String.Format(_notExistTypeMessage,value));
                _type = value;
            }
            get
            {
                return _type;
            }
        }
        public string ISBN 
        { 
            set
            { 
                if (Regex.IsMatch(value, _ISBNMask) == false)
                    throw new ArgumentException(String.Format(_invalidISBNMessage,value));
                _ISBN = value;
            }
            get 
            {
                return _ISBN;
            } 
        }
        public ISet<string> Genres
        {
            set
            {
                foreach (var genre in value)
                    if (GenreConsts.Contains(genre) == false)
                        throw new ArgumentOutOfRangeException(String.Format(_notExistGenreMessage,value));
                _genres = value;
            }
            get
            {
                return _genres;
            }
        }
        public string OriginalName { set; get; }
        public int? PageQuantity 
        { 
            set 
            {
                if (value < _minPageQuantity)
                    throw new ArgumentOutOfRangeException(_minPageQuantityMessage);
                if (value < _maxPageQuantity)
                    throw new ArgumentOutOfRangeException(_maxPageQuantityMessage);
                _pageQuantity = value;
            } 
            get 
            {
                return _pageQuantity;
            } 
        }
        public string CoverArt
        {
            set
            {
                if (CoverArtConsts.Contains(value) == false)
                    throw new ArgumentOutOfRangeException(String.Format(_notExistCoverArtMessage,value));
                _coverArt = value;
            }

            get
            {
                return _coverArt;
            }
        }
        public string AgeLimit
        {
            set
            {
                if (AgeLimitConsts.Contains(value) == false)
                    throw new ArgumentOutOfRangeException(String.Format(_notExistAgeLimitMessage,value));
                _ageLimit = value;
            }
            get
            {
                return _ageLimit;
            }
        }
        public string PublicationFormat
        {
            set
            {
                if (Regex.IsMatch(value, _formatMask) == false)
                    throw new ArgumentException(String.Format(_invalidFormatMessage, value));
                _format = value;
            }
            get
            {
                return _format;
            }
        }
    }
}
