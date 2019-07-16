using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
  public class Drink
  {
    public uint Id { get; set; }
    public string Name { get; set; }
    public uint Price { get; set; }
    public uint Count { get; set; }
  }
}