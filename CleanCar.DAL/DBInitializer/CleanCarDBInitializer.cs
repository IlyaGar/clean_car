using CleanCar.DAL.Context;
using CleanCar.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.DAL.DBInitializer
{
    public class CleanCarDBInitializer : DropCreateDatabaseAlways<CleanCarContext>
    {
        protected override void Seed(CleanCarContext context)
        {
            IList<Customer> defaultCustomers = new List<Customer>()
            {    
                new Customer
                {
                    Id = 1,
                    FirstName = "John",
                    SecondName = "Wick",
                    Phone = "+12345679",
                    CreateDate = DateTime.Now.AddDays(-7)
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Leslie",
                    SecondName = "Knope",
                    Phone = "+135465412",
                    CreateDate = DateTime.Now.AddDays(-6)
                },
                new Customer
                {
                    Id = 3,
                    FirstName = "Michael",
                    SecondName = "Scott",
                    Phone = "+126454545",
                    CreateDate = DateTime.Now.AddDays(-5)
                },
                new Customer
                {
                    Id = 4,
                    FirstName = "Pamela",
                    SecondName = "Halpert",
                    Phone = "+129887546",
                    CreateDate = DateTime.Now.AddDays(-4)
                }
            };
            IList<Operation> defaultOperations = new List<Operation>()
            {
                new Operation
                {
                    Id = 1,
                    Name = "Стандарт",
                    Price = 13,
                },
                new Operation
                {
                    Id = 2,
                    Name = "Эконом",
                    Price = 10,
                },
                new Operation
                {
                    Id = 3,
                    Name = "Люкс",
                    Price = 20,
                },
                new Operation
                {
                    Id = 4,
                    Name = "Авто",
                    Price = 10,
                },
                new Operation
                {
                    Id = 5,
                    Name = "Пылесос",
                    Price = 12,
                }
            };
            IList<Order> defaultOrders = new List<Order>()
            {
                new Order
                {
                    Id = 1,
                    OperationId = 1,
                    DateTime = DateTime.Now.AddDays(-3),
                    CustomerId = 1,
                    Price = 13
                },
                new Order
                {
                    Id = 2,
                    OperationId = 5,
                    DateTime = DateTime.Now.AddDays(-3),
                    CustomerId = 1,
                    Price = 12
                },
                new Order
                {
                    Id = 3,
                    OperationId = 2,
                    DateTime = DateTime.Now.AddDays(-2),
                    CustomerId = 2,
                    Price = 10
                },
                new Order
                {
                    Id = 4,
                    OperationId = 3,
                    DateTime = DateTime.Now.AddDays(-2),
                    CustomerId = 3,
                    Price = 20
                },
                new Order
                {
                    Id = 5,
                    OperationId = 4,
                    DateTime = DateTime.Now.AddDays(-3),
                    CustomerId = 4,
                    Price = 10
                },
                new Order
                {
                    Id = 6,
                    OperationId = 5,
                    DateTime = DateTime.Now.AddDays(-1),
                    CustomerId = 4,
                    Price = 12
                }
            };

            context.Customers.AddRange(defaultCustomers);
            context.Operations.AddRange(defaultOperations);
            context.Orders.AddRange(defaultOrders);

            base.Seed(context);
        }
    }
}