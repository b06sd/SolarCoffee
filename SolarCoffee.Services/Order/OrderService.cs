using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public OrderService(SolarDbContext db, 
            ILogger<OrderService> logger, 
            IProductService productService, 
            IInventoryService inventoryService)
        {
            _db = db;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }


        /// <summary>
        /// Creates an open SalesOrder
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            var orderId = order.Id;
            _logger.LogInformation($"Generating new order {orderId}"); 

            foreach (var item in order.SalesOrderItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);

                var inventoryId = _inventoryService.GetByProductId(item.Product.Id).Id;

                _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
            }
            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = "Open Order Created",
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {

                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = ex.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }

        /// <summary>
        /// Get all SalesOrder from the systems
        /// </summary>
        /// <returns></returns>
        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include( so => so.Customer)
                    .ThenInclude(customer => customer.PrimaryAddress)
                .Include( so => so.SalesOrderItems)
                    .ThenInclude(item => item.Product)
                .ToList();
        }

        /// <summary>
        /// Marks an open SaleOrder as paid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var order = _db.SalesOrders.Find(id);
            order.UpdatedOn = DateTime.UtcNow;
            order.IsPaid = true;
            try
            {
                _db.SalesOrders.Update(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = $"Order {order.Id} Closed: Invoice paid in full.",
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {

                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = ex.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }
    }
}
