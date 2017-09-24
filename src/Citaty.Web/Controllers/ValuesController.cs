﻿using System.Collections.Generic;
using Citaty.Core.Services;
using Microsoft.AspNetCore.Mvc;

//using Citaty.Core.Services;

namespace Citaty.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IValueService _valueService;

        public ValuesController(IValueService valueService)
        {
            _valueService = valueService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _valueService.GetValues();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}