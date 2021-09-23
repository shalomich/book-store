using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.WebApi.Areas.Dashboard.ViewModels.Forms
{
    public interface IEntityForm
    {
        public int Id { init; get; }

        public string Name { init; get; }
    }
}
