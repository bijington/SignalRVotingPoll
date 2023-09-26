﻿namespace SignalRVotingPoll;

public class VoteState
{
    public string Title { get; set; } = string.Empty;

    public string Option1Label { get; set; } = string.Empty;

    public string Option2Label { get; set; } = string.Empty;

    public bool IsOpen { get; set; }

    public Dictionary<int, int> Votes
    {
        get;
    } = new(new Dictionary<int, int> { { 0, 0 }, { 1, 0 } });
}
