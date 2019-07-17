using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
    public class VendingMachineContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        private VendingMachineContext() : base("name=VendingMachineContext")
        {

        }

        public VendingMachineContext(RunTime a_runTime)
        {
            RunTime = a_runTime;
        }

        public System.Data.Entity.DbSet<Drink> Drinks { get; set; }
        public System.Data.Entity.DbSet<Coin> Coins { get; set; }

        public RunTime RunTime { get; private set; }

    }
}
