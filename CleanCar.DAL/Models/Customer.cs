using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCar.DAL.Models
{
    /// <summary>
    /// Represents a car wash customer.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets a customer ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a customer first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a customer second name.
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Gets or sets a customer phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets a date of creation customer.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets a collection of customer`s operations.
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; }
    }
}