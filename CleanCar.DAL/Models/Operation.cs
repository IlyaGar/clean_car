using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCar.DAL.Models
{
    /// <summary>
    /// Represents a car wash operation.
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Gets or sets an operation ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets an operation name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets an operation price.
        /// </summary>
        public decimal Price { get; set; }
    }
}
