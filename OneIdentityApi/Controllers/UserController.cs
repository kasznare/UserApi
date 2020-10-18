using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneIdentityApi.Models;
using OneIdentityApi.Services;

namespace OneIdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public Task<List<User>> GetAllUsers()
        {
            return _userService.GetUsersAsync();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<User> GetUserById(string id)
        {
            return await _userService.GetUserByIdAsync(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState); //validation for later
            await _userService.AddUserAsync(user);

            return user.id != null ? (IActionResult)Ok(user.id) : BadRequest();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void UpdateUser(int id, [FromBody] User value)
        {
            _userService.UpdateUser(value);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userService.DeleteUser(id);
        }
    }
}
