namespace TaskBoard.Models;

public class Board
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int WorkspaceId { get; set; }
}
