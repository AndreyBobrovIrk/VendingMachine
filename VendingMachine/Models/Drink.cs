using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Models
{
    public class Drink
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Введите количество")]
        public int Count { get; set; }
    }
}