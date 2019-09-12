using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Common
{
    public enum OrderStatus
    {
        Open = 0,
        Confirmed = 1,
        Paid = 2,
        Done = 3,
        Cancel = 4
    }

    public enum PaymentMethod
    {
        COD, CreditCard, Tranfer,
        InternetBanking, SMSBanking, MobileBanking
    }
}
