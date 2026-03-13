using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TaskBoard.Data;
using TaskBoard.Models;

namespace TaskBoard.Controllers;

[ApiController]
[Route("api/lists")]
[Authorize]
public class ListController : ControllerBase
{
    private readonly AppDbContext _context;

    public ListController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/lists/board/{boardId}
    [HttpGet("board/{boardId}")]
    public IActionResult GetLists(int boardId)
    {
        var lists = _context.Lists
            .Where(l => l.BoardId == boardId)
            .OrderBy(l => l.Position)
            .ToList();

        return Ok(lists);
    }

    // POST /api/lists
    [HttpPost]
    public IActionResult CreateList(BoardList list)
    {
        _context.Lists.Add(list);
        _context.SaveChanges();

        return Ok(list);
    }

    // DELETE /api/lists/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteList(int id)
    {
        var list = _context.Lists.Find(id);

        if (list == null)
            return NotFound();

        _context.Lists.Remove(list);
        _context.SaveChanges();

        return Ok();
    }
}
