﻿using App.Areas.Dashboard.ViewModels;
using App.Entities;
using App.Entities.Books;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Areas.Dashboard.Profiles
{
    public class FormToBookProfile : Profile
    {
        public FormToBookProfile()
        {
            CreateMap<BookForm, Book>()
                .ForMember(book => book.GenresBooks,
                    mapper => mapper.MapFrom(form => form.GenreIds
                    .Select(id => new GenreBook { GenreId = id })))
            .ReverseMap()
                .ForMember(form => form.GenreIds,
                    mapper => mapper.MapFrom(book => book.GenresBooks
                        .Select(genre => genre.GenreId)));

            CreateMap<AuthorForm, Author>()
              .ReverseMap();

            CreateMap<PublisherForm, Publisher>()
              .ReverseMap();

            CreateMap<GenreForm, Genre>()
              .ReverseMap();

            
            CreateMap<CoverArtForm, CoverArt>()
              .ReverseMap();

            CreateMap<BookTypeForm, BookType>()
              .ReverseMap();

            CreateMap<AgeLimitForm, AgeLimit>()
              .ReverseMap();   
        }
    }
}
