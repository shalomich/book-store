﻿using System;
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

        private const string _ISBNMask = @"^978-5-\d{6}-\d{2}-\d{1}$";
        private const string _formatMask = @"^[1-9]\d{1}x[1-9]\d{1}x[1-9]$";
        private const string _ISBNSchema = "978-5-XXXXXX-XX-X";
        private const string _formatSchema = "(10-99)X(10-99)X(1-9)";

        private static readonly string _notExistTypeMessage;
        private static readonly string _notExistGenreMessage;
        private static readonly string _notExistCoverArtMessage;
        private static readonly string _notExistAgeLimitMessage;
        private static readonly string _minReleaseYearMessage;
        private static readonly string _maxReleaseYearMessage;
        private static readonly string _minPageQuantityMessage;
        private static readonly string _maxPageQuantityMessage;
        private static readonly string _invalidISBNMessage;
        private static readonly string _invalidFormatMessage;

        private int _releaseYear;
        private string _type;
        private string _ISBN;
        private ISet<string> _genres;
        private int? _pageQuantity;
        private string _coverArt;
        private string _ageLimit;
        private string _format;

        static Publication()
        {
            _notExistTypeMessage = ExceptionMessages.GetMessage(ExceptionMessages.MessageType.NotExist, "Type", String.Join(" ", TypeConsts));
            _notExistGenreMessage = ExceptionMessages.GetMessage(ExceptionMessages.MessageType.NotExist, "Genre", String.Join(" ", GenreConsts));
            _notExistCoverArtMessage = ExceptionMessages.GetMessage(ExceptionMessages.MessageType.NotExist, "CoverArt", String.Join(" ", CoverArtConsts));
            _notExistAgeLimitMessage = ExceptionMessages.GetMessage(ExceptionMessages.MessageType.NotExist, "AgeLimit", String.Join(" ", AgeLimitConsts));

            _invalidISBNMessage = ExceptionMessages.GetMessage(ExceptionMessages.MessageType.Invalid, "ISBN", _ISBNSchema);
            _invalidFormatMessage = ExceptionMessages.GetMessage(ExceptionMessages.MessageType.Invalid, "Format", _formatSchema);

            _maxReleaseYearMessage = ExceptionMessages.GetMessage(ExceptionMessages.MessageType.More, "ReleaseYear", _maxReleaseYear.ToString());
            _maxPageQuantityMessage = ExceptionMessages.GetMessage(ExceptionMessages.MessageType.More, "PageQuantity", _maxPageQuantity.ToString());

            _minReleaseYearMessage = ExceptionMessages.GetMessage(ExceptionMessages.MessageType.Less, "ReleaseYear", _minReleaseYear.ToString());
            _minPageQuantityMessage = ExceptionMessages.GetMessage(ExceptionMessages.MessageType.Less, "PageQuantity", _minPageQuantity.ToString());
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

        public Author Author { set; get; }
        public int AuthorId { set; get; }
        public Publisher Publisher { set; get; }
        public int PublisherId { set; get; }

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
        public string ISBN 
        { 
            set
            { 
                if (Regex.IsMatch(value, _ISBNMask) == false)
                    throw new ArgumentException(_invalidISBNMessage);
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
                        throw new ArgumentOutOfRangeException(_notExistGenreMessage);
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
                    throw new ArgumentOutOfRangeException(_notExistCoverArtMessage);
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
                    throw new ArgumentOutOfRangeException(_notExistAgeLimitMessage);
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
                    throw new ArgumentException(_invalidFormatMessage);
                _format = value;
            }
            get
            {
                return _format;
            }
        }
    }
}