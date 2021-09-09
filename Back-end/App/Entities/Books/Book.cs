﻿



























































using App.Entities.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Entities
{
    public class Book : Product
    {
        public const int MinReleaseYear = 2000;
        public static readonly int MaxReleaseYear = DateTime.Today.Year;

        public const string IsbnTemplate = @"^978-5-\d{6}-\d{2}-\d{1}$";
        public const string FormatTemplate = @"^[1-9]\d{1}x[1-9]\d{1}x[1-9]$";

        private const string IsbnSchema = "978-5-XXXXXX-XX-X";       
        private const string FormatSchema = "(10-99)x(10-99)x(1-9)";

        private int _releaseYear;
        private string _Isbn;
        private string _format;
        private int? _pageQuantity;

        public string Isbn
        {
            set
            {
                if (Regex.IsMatch(value, IsbnTemplate) == false)
                    throw new ArgumentException();
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
                    throw new ArgumentOutOfRangeException();
                if (value > MaxReleaseYear)
                    throw new ArgumentOutOfRangeException();
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

        public virtual BookType Type { set; get; }
        public int? TypeId { set; get; }

        public virtual ISet<GenreBook> GenresBooks { set; get; }

        public ISet<string> Genres => GenresBooks
            .Select(genreBook => genreBook.Genre.Name)
            .ToHashSet();

        public string OriginalName { set; get; }

        public virtual AgeLimit AgeLimit { set; get; }
        public int? AgeLimitId { set; get; }

        public virtual CoverArt CoverArt { set; get; }
        public int? CoverArtId { set; get; }

        public string BookFormat
        {
            set
            {
                if (value != null) 
                    if (Regex.IsMatch(value, FormatTemplate) == false)
                        throw new ArgumentException();
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