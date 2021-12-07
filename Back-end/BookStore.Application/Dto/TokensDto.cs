using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Dto
{
    public record TokensDto(string AccessToken, string RefreshToken);
}
