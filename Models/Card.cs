namespace TaskBoard.Models;

public class Card
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public int ListId { get; set; }
}
