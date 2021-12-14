using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Providers
{
    public interface IJwtProvider
    {
        public JwtSettings GenerateSettings();
    }
}
