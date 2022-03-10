using AuthorsAngularTask.Dtos;
using AuthorsAngularTask.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAngularTask.Profiles
{
    public class ModelsProfile:Profile
    {

        public ModelsProfile()
        {
            CreateMap<Book, BooksCreate>();
            CreateMap<Author, AuthorCreate>();
            CreateMap<BooksCreate,Book >();
            CreateMap<AuthorCreate,Author >();
        }
    }
}
