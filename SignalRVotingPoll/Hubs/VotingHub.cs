using Microsoft.AspNetCore.SignalR;

namespace SignalRVotingPoll.Hubs;

public class VotingHub : Hub
{
    private readonly VoteState _voteState;
    private static readonly IList<string> _connectionIds = new List<string>();

    public VotingHub(VoteState voteState)
    {
        _voteState = voteState;
    }

    public async Task OpenVoting(VoteState voteState)
    {
        _voteState.Option1Label = voteState.Option1Label;
        _voteState.Option2Label = voteState.Option2Label;
        _voteState.Title = voteState.Title;
        _voteState.Votes[0] = 0;
        _voteState.Votes[1] = 0;
        _voteState.IsOpen = true;
        _voteState.SelectWinner = voteState.SelectWinner;

        await Clients.All.SendAsync("Status", _voteState);
        await Clients.All.SendAsync("UpdateVotes", _voteState.Votes);
    }

    public async Task GetVotingStatus()
    {
        await Clients.All.SendAsync("Status", _voteState);
    }

    public async Task CloseVoting()
    {
        _voteState.IsOpen = false;

        await Clients.All.SendAsync("Status", _voteState);

        if (_voteState.SelectWinner)
        {
            // Make sure to exclude our own connection as that shouldn't win.
            var connections = _connectionIds.Except(new List<string> { Context.ConnectionId }).ToList();
            
            Random random = new();
            var winningIndex = random.Next(connections.Count + 1);

            var winningConnectionId = connections[winningIndex];

            await Clients.Client(winningConnectionId).SendAsync("Congratulations");
        }
    }

    public override Task OnConnectedAsync()
    {
        _connectionIds.Add(Context.ConnectionId);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _connectionIds.Remove(Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendVote(int option)
    {
        // TODO implement server-side user identification to prevent duplicate votes
        if (_voteState.IsOpen is false)
        {
            return;
        }

        int opt = option;

        _voteState.Votes[opt] += 1;

        await Clients.All.SendAsync("UpdateVotes", _voteState.Votes);
    }
}
