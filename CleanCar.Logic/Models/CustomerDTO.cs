using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCar.Logic.Models
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Phone { get; set; }
        public int CountOperations { get; set; }
        public decimal SumPrice { get; set; }
    }
}
