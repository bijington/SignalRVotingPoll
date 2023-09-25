using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SignalRVotingPoll.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public string Question { get; set; } = string.Empty;

    [BindProperty]
    public string Option1 { get; set; } = string.Empty;

    [BindProperty]
    public string Option2 { get; set; } = string.Empty;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet([FromQuery] int voteId = -1)
    {
        if (voteId == 0)
        {
            Question = "Would you like the red or the blue pill?";
            Option1 = "The red pill";
            Option2 = "The blue pill";
        }
        else if (voteId == 1)
        {
            Question = "Fill this one out";
            Option1 = "The red pill";
            Option2 = "The blue pill";
        }
        else if (voteId == 2)
        {
            Question = "And this one";
            Option1 = "The red pill";
            Option2 = "The blue pill";
        }
    }
}

