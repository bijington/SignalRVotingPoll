"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/votingHub").build();

document.getElementById("option1Button").addEventListener("click", function (event) {
    connection.invoke("SendVote", 0).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("statusLabel").innerText = "👍 Thanks for your vote, voting is now disabled.";
    document.getElementById("option1Button").disabled = true;
    document.getElementById("option2Button").disabled = true;

    event.preventDefault();
});

connection.on("Reset", function () {
    document.getElementById("statusLabel").innerText = "🔄 Voting has been reset! Happy voting!";
    document.getElementById("mehButton").disabled = false;
    document.getElementById("awesomeButton").disabled = false;
});

document.getElementById("option2Button").addEventListener("click", function (event) {
    connection.invoke("SendVote", 1).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("statusLabel").innerText = "👍 Thanks for your vote, voting is now disabled.";
    document.getElementById("option1Button").disabled = true;
    document.getElementById("option2Button").disabled = true;

    event.preventDefault();
});

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});

