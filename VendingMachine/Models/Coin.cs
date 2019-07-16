using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
    public class Coin
    {
        uint Id { get; set; }
        bool IsEnabled { get; set; }
    }
}