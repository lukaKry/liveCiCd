﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebApplication2.Data;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerContext context;

        public CustomersController(CustomerContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAll() => await context.Customers.ToArrayAsync();

        [HttpPost]
        public async Task<Customer> Add ( [FromBody] Customer c)
        {
            context.Customers.Add(c);
            await context.SaveChangesAsync();
            return c;
        }
    }
}
