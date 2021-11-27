using Abp.Specifications;
using BookStore.Domain.Entities.Books;
using QueryWorker.DataTransformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.DbQueryConfigs.SelectionConfigs
{
    public interface ISelectionConfig
    {
        Specification<Book> CreateSpecification();
        Sorting<Book> CreateSorting();
    }
}
