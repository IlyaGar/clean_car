using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCar.Logic.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
    }
}
