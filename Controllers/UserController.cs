using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data;
using TaskBoard.Models;

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
}
