using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Enums
{
    public enum PaymentMethod
    {
        [EnumMember(Value = "Онлайн")]
        Online,

        [EnumMember(Value = "Оффлайн")]
        Offline
    }
}
