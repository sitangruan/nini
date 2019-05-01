using Microsoft.AspNetCore.Mvc;
using nini.core.V10;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;

namespace nini.V10.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "FirstLevel")]
    public class ValuesController : ControllerBase
    {
        private readonly IValuesManager _valuesManager;

        public ValuesController(IValuesManager manager)
        {
            _valuesManager = manager;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var result = _valuesManager.GetValues();
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var result = _valuesManager.GetValue(id);
            return result;
        }

        // POST api/values
        [HttpPost]
        public OkResult Post([FromBody] string value)
        {
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        public NoContentResult Put(int id, [FromBody] string value)
        {
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        public NoContentResult Delete(int id)
        {
            return NoContent();
        }
    }
}
