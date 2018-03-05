using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestAppWebapiCore.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger _logger;
        
        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var ikey = TelemetryConfiguration.Active.InstrumentationKey;
            var ep = TelemetryConfiguration.Active.TelemetryChannel.EndpointAddress;

            _logger.LogWarning("Logging... Endpoint is ({ep})", ep);


            HttpClient client = new HttpClient();
                var res = client.GetStringAsync("https://bing.com").Result;
            return new string[] { "value1", "value2", ikey, ep };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _logger.LogWarning(1001, "Logging... Get called ({id})", id);
            return "value" + id ;
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
