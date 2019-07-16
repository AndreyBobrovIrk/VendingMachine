using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
    public class Coin : IIdentified, IValuable
    {
        public int Id { get; set; }
        public bool Disabled { get; set; }
        public int Value { get; set; }
    }
}