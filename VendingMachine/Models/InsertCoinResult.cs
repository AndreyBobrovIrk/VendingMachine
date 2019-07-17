using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VendingMachine.Models
{
    public class InsertCoinResult : ActionResult
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {

        }
    }
}