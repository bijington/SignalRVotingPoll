using Microsoft.AspNetCore.SignalR;

namespace SignalRVotingPoll.Hubs;

public class VotingHub : Hub
{
    private static VoteState _voteState = new VoteState();

    public VotingHub()
    {
    }

    public async Task OpenVoting(VoteState voteState)
    {
        _voteState = voteState;

        _voteState.Votes[0] = 0;
        _voteState.Votes[1] = 0;
        _voteState.IsOpen = true;

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

