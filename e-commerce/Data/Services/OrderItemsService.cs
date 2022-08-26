using e_commerce.Data.Base;
using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Data.Services
{
    public class OrderItemsService : EntityBaseRepository<OrderItem>,IOrderItemsService
    {

        public OrderItemsService(AppDbContext context) : base(context)
        {
        }
    }
}
