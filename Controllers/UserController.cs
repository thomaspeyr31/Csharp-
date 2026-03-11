using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data;
using TaskBoard.Models;
using BCrypt.Net;

namespace TaskBoard.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/users
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }

    // POST /api/users
    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(user);
    }

    // POST /api/users/register
    [HttpPost("register")]
    public IActionResult Register(User user)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);

        if (existingUser != null)
        {
            return BadRequest("User already exists");
        }

        // hash du mot de passe
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(user);
    }
}
