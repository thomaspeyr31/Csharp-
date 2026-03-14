using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TaskBoard.Data;
using TaskBoard.Models;

namespace TaskBoard.Controllers;

[ApiController]
[Route("api/cards")]
[Authorize]
public class CardController : ControllerBase
{
    private readonly AppDbContext _context;

    public CardController(AppDbContext context)
    {
        _context = context;
    }

    // récupérer les cartes d'une list
    [HttpGet("list/{listId}")]
    public IActionResult GetCardsByList(int listId)
    {
        var cards = _context.Cards
            .Where(c => c.ListId == listId)
            .OrderBy(c => c.Position)
            .ToList();

        return Ok(cards);
    }

    // créer une carte
    [HttpPost]
    public IActionResult CreateCard(Card card)
    {
        _context.Cards.Add(card);
        _context.SaveChanges();

        return Ok(card);
    }

    // modifier une carte
    [HttpPut("{id}")]
    public IActionResult UpdateCard(int id, Card updatedCard)
    {
        var card = _context.Cards.Find(id);

        if (card == null)
            return NotFound();

        card.Title = updatedCard.Title;
        card.Description = updatedCard.Description;
        card.Position = updatedCard.Position;
        card.ListId = updatedCard.ListId;

        _context.SaveChanges();

        return Ok(card);
    }

    // supprimer une carte
    [HttpDelete("{id}")]
    public IActionResult DeleteCard(int id)
    {
        var card = _context.Cards.Find(id);

        if (card == null)
            return NotFound();

        _context.Cards.Remove(card);
        _context.SaveChanges();

        return Ok();
    }
}
