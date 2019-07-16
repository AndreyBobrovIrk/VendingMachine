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
            SelectedDrinks = new SelectedCollection<Drink>();
            SelectedCoins = new SelectedCollection<Coin>();
        }

        public SelectedCollection<Coin> SelectedCoins { get; set; }
        public SelectedCollection<Drink> SelectedDrinks { get; set; }
        public bool IsAdmin { get; set; }
    }
}