using Microsoft.AspNetCore.Mvc;
using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private static readonly Dictionary<string, string> States = new Dictionary<string, string>
        {
            { "MI", "Michigan" }
        };

        [HttpGet]
        public IActionResult GetStates()
        {
            var statesList = States.Select(state => new States
            {
                Abbreviation = state.Key,
                Name = state.Value
            });

            return Ok(statesList);
        }
    }
}
