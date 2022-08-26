using e_commerce.Data.Services;
using e_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    public class OrderItems
    {
        private readonly IOrderItemsService _service;

        public async Task Create ([Bind("Id,Quantity,MovieId")]OrderItem orderItem)
        {
            await _service.Add(orderItem);
        }

    }
}
