using GFDSystems.Vigitech.API.Tools.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiKey(Policies = new [] { "Administrador", "Operador" })]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>   
        [HttpDelete("{id}")]
        //[Authorize(Policy = "Claim.DoB")]
        public IActionResult Delete(long id)
        {
            return NoContent();
        }
    }
}