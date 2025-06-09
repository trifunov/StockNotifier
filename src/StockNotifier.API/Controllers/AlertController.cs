using Microsoft.AspNetCore.Mvc;
using StockNotifier.Application.Commands.CreateAlert;
using StockNotifier.Application.Core.Command;
using StockNotifier.Application.Core.Query;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockNotifier.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public AlertController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        // GET: api/<AlertController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AlertController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AlertController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAlertRequest command, CancellationToken cancellationToken)
        {
            var result = await _commandDispatcher.SendAsync<bool, CreateAlertRequest>(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        // PUT api/<AlertController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AlertController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
