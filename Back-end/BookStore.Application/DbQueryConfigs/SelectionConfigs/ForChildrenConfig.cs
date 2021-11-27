using Abp.Specifications;
using BookStore.Application.DbQueryConfigs.Specifications;
using BookStore.Domain.Entities.Books;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.SelectionConfigs
{
    public class ForChildrenConfig : ISelectionConfig
    {
        public Sorting<Book> CreateSorting()
        {
            return new Sorting<Book>(book => Guid.NewGuid());
        }

        public Specification<Book> CreateSpecification()
        {
            return new ForChildrenSpecification();
        }
    }
}
