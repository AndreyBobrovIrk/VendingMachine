using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
    public interface IIdentified
    {
        int Id { get; set; }
    }

    public interface IValuable
    {
        int Value { get; set; }
    }
}