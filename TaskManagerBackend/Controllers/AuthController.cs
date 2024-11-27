using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.Models;
using TaskManagerBackend.Dtos;
using System.Linq;
using BCrypt.Net;

namespace TaskManagerBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto userDto)
        {
            if (_context.Users.Any(u => u.Email == userDto.Email))
                return BadRequest("Email já está em uso.");

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Usuário registrado com sucesso!");
        }

        [HttpPost("login")]
        public IActionResult Login(UserDto userDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
                return Unauthorized("Email ou senha inválidos.");

            return Ok(new { Message = "Login realizado com sucesso!", UserId = user.Id });
        }
    }
}
