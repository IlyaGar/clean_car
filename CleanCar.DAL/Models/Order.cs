using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCar.DAL.Models
{
    /// <summary>
    /// Represents a customer operation.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets a customer operation ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a customer operation date and time.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets a customer operation price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets an ID of operation.
        /// </summary>
        public int? OperationId { get; set; }

        /// <summary>
        /// Gets or sets an operatin for customer.
        /// </summary>
        public virtual Operation Operation { get; set; }

        /// <summary>
        /// Gets or sets an ID of customer.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets a customer for customer operatin.
        /// </summary>
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
