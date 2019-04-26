using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Macaw_KeyVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly string _keyVaultValue;

        public ValuesController(IConfiguration configuration)
        {
            _keyVaultValue = configuration["name-of-secret-in-key-vault"];
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] {"value1", "value2", _keyVaultValue};
        }
    }
}