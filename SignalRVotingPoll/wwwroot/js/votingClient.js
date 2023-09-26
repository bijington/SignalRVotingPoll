"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/votingHub").build();

document.getElementById("option1Button").addEventListener("click", function (event) {
    connection.invoke("SendVote", 0).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("statusLabel").innerText = "👍 Thanks for your vote, voting is now disabled.";
    document.getElementById("option1Button").disabled = true;
    document.getElementById("option2Button").disabled = true;
    document.getElementById("option1Button").hidden = true;
    document.getElementById("option2Button").hidden = true;

    event.preventDefault();
});

connection.on("Status", function (voteState) {
    if (voteState.isOpen) {
        document.getElementById("statusLabel").innerText = "Voting has been opened! Happy voting!";
        document.getElementById("titleLabel").innerText = voteState.title;
        document.getElementById("option1Button").innerText = voteState.option1Label;
        document.getElementById("option2Button").innerText = voteState.option2Label;
    }
    else {
        document.getElementById("statusLabel").innerText = "Voting is closed!";
        document.getElementById("titleLabel").innerText = "";
        document.getElementById("option1Button").innerText = "";
        document.getElementById("option2Button").innerText = "";
    }

    document.getElementById("option1Button").disabled = !voteState.isOpen;
    document.getElementById("option2Button").disabled = !voteState.isOpen;
    document.getElementById("option1Button").hidden = !voteState.isOpen;
    document.getElementById("option2Button").hidden = !voteState.isOpen;
});

connection.on("Reset", function () {
    document.getElementById("statusLabel").innerText = "🔄 Voting has been reset! Happy voting!";
    document.getElementById("option1Button").disabled = false;
    document.getElementById("option2Button").disabled = false;
});

document.getElementById("option2Button").addEventListener("click", function (event) {
    connection.invoke("SendVote", 1).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("statusLabel").innerText = "👍 Thanks for your vote, voting is now disabled.";
    document.getElementById("option1Button").disabled = true;
    document.getElementById("option2Button").disabled = true;
    document.getElementById("option1Button").hidden = true;
    document.getElementById("option2Button").hidden = true;

    event.preventDefault();
});

connection.start().then(function () {
    connection.invoke("GetVotingStatus").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

