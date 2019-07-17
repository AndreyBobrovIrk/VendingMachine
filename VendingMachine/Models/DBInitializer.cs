using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VendingMachine.Models
{
  public class DBInitializer : DropCreateDatabaseIfModelChanges<VendingMachineContext>
  {
    protected override void Seed(VendingMachineContext context)
    {
      var drinks = new List<Drink>
            {
                new Drink { Id = 0, Name = "Cola", Price = 30 },
                new Drink { Id = 1, Name = "Pepsi", Price = 45 },
                new Drink { Id = 2, Name = "Beer", Price = 100 },
            };
      drinks.ForEach(s => context.Drinks.Add(s));
      context.SaveChanges();
    }
  }
}