using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Counter.Command;
using Counter.DomainObjects;
using Counter.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Counter.API.Controllers
{
    [Route("api/Counter")]
    public class CounterController : Controller
    {
        private readonly CounterCommand _incrementCommand;
        private readonly CounterRepository _counterReposotory;
        public CounterController(CounterCommand incrementCommand, CounterRepository counterReposotory)
        {
            _incrementCommand = incrementCommand;
            _counterReposotory = counterReposotory;
        }
        /// <summary>
        /// Get counter
        /// </summary>
        /// <returns></returns>
        // GET: api/Counter
        [HttpGet]
        public async Task<DomainObjects.Counter> Get()
        {
            var counter = await _counterReposotory.Get();
            return counter;
        }
        /// <summary>
        /// Increment counter by 1
        /// </summary>
        // POST api/values
        [HttpPost]
        public async void Post()
        {
            await _incrementCommand.Execute();
        }
    }
}
