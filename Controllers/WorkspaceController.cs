using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TaskBoard.Data;
using TaskBoard.Models;

namespace TaskBoard.Controllers;

[ApiController]
[Route("api/workspaces")]
[Authorize]
public class WorkspaceController : ControllerBase
{
    private readonly AppDbContext _context;

    public WorkspaceController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/workspaces
    [HttpGet]
    public IActionResult GetWorkspaces()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var workspaces = _context.Workspaces
            .Where(w => w.OwnerId == userId)
            .ToList();

        return Ok(workspaces);
    }

    // POST /api/workspaces
    [HttpPost]
    public IActionResult CreateWorkspace(Workspace workspace)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        workspace.OwnerId = userId;

        _context.Workspaces.Add(workspace);
        _context.SaveChanges();

        return Ok(workspace);
    }

    // DELETE /api/workspaces/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteWorkspace(int id)
    {
        var workspace = _context.Workspaces.Find(id);

        if (workspace == null)
        {
            return NotFound();
        }

        _context.Workspaces.Remove(workspace);
        _context.SaveChanges();

        return Ok();
    }
}
