using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
    public class RunTime
    {
        public RunTime()
        {
            SelectedDrinks = new SelectedDrinkCollection();
        }

        public SelectedDrinkCollection SelectedDrinks { get; set; }
        public int Coins { get; set; }
        public int CoinsLimit { get { return Coins - SelectedDrinks.Total; } }
        public bool IsAdmin { get; set; }
    }
}