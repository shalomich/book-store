﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Various;

namespace Storage.Models
{
    public class Publication : Product
    {
        private const int _minReleaseYear = 2000;
        private static readonly int _maxReleaseYear = DateTime.Today.Year;
        private const int _minPageQuantity = 1;
        private const int _maxPageQuantity = int.MaxValue;
        private const int _maxWeight = 10000;

        private const string _IsbnMask = @"^978-5-\d{6}-\d{2}-\d{1}$";
        private const string _formatMask = @"^[1-9]\d{1}(\.\d{1})x[1-9]\d{1}(\.\d{1})x[1-9](\.\d{1})$";

        private const string _IsbnSchema = "978-5-XXXXXX-XX-X";       
        private const string _formatSchema = "[10.0-99.9]x[10.0-99.9]x[1.0-9.9]";

        private static readonly string _notExistTypeMessage;
        private static readonly string _notExistGenreMessage;
        private static readonly string _notExistCoverArtMessage;
        private static readonly string _notExistAgeLimitMessage;
        private static readonly string _minReleaseYearMessage;
        private static readonly string _maxReleaseYearMessage;
        private static readonly string _minCirculationMessage;
        private static readonly string _minWeightMessage;
        private static readonly string _maxWeightMessage;
        private static readonly string _minPageQuantityMessage;
        private static readonly string _maxPageQuantityMessage;
        private static readonly string _invalidIsbnMessage;
        private static readonly string _invalidFormatMessage;

        private int _releaseYear;
        private string _type;
        private string _Isbn;
        private ISet<string> _genres;
        private int? _circulation;
        private int? _weight; 
        private int? _pageQuantity;
        private string _coverArt;
        private string _ageLimit;
        private string _format;

        static Publication()
        {
            _notExistTypeMessage = ExceptionMessages.GetMessage(ExceptionMessageType.NotExist, nameof(Type), String.Join(" ", TypeConsts));
            _notExistGenreMessage = ExceptionMessages.GetMessage(ExceptionMessageType.NotExist, "Genre", String.Join(" ", GenreConsts));
            _notExistCoverArtMessage = ExceptionMessages.GetMessage(ExceptionMessageType.NotExist, nameof(CoverArt), String.Join(" ", CoverArtConsts));
            _notExistAgeLimitMessage = ExceptionMessages.GetMessage(ExceptionMessageType.NotExist, nameof(AgeLimit), String.Join(" ", AgeLimitConsts));

            _invalidIsbnMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Invalid, nameof(Isbn), _IsbnSchema);
            _invalidFormatMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Invalid, nameof(Format), _formatSchema);

            _maxReleaseYearMessage = ExceptionMessages.GetMessage(ExceptionMessageType.More, nameof(ReleaseYear), _maxReleaseYear.ToString());
            _maxPageQuantityMessage = ExceptionMessages.GetMessage(ExceptionMessageType.More, nameof(PageQuantity), _maxPageQuantity.ToString());
            _maxWeightMessage = ExceptionMessages.GetMessage(ExceptionMessageType.More, nameof(Weight), _maxWeight.ToString());

            _minReleaseYearMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Less, nameof(ReleaseYear), _minReleaseYear.ToString());
            _minPageQuantityMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Less, nameof(PageQuantity), _minPageQuantity.ToString());
            _minCirculationMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Less, nameof(Circulation), Convert.ToString(0));
            _minWeightMessage = ExceptionMessages.GetMessage(ExceptionMessageType.Less, nameof(Weight), Convert.ToString(0));
        }

        public static readonly string[] TypeConsts =
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
            "Твёрдый переплёт",
            "Мягкий переплёт"
        };

        public static readonly string[] AgeLimitConsts = 
        {
            "0+",
            "6+",
            "12+",
            "18+"
        };
     
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
        
        public string Format
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

        public int? Weight
        {
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(_minWeightMessage);
                if (value > _maxWeight)
                    throw new ArgumentOutOfRangeException(_maxWeightMessage);
                _weight = value;
            }

            get
            {
                return _weight;
            }
        }
        public int? Circulation
        {
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(_minCirculationMessage);
                _circulation = value;
            }

            get
            {
                return _circulation;
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

        public string Type
        {
            set
            {
                if (TypeConsts.Contains(value) == false)
                    throw new ArgumentOutOfRangeException(_notExistTypeMessage);
                _type = value;
            }
            get
            {
                return _type;
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
    }
}
