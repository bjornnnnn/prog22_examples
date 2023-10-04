"use strict";


let token;

async function fetchToken() {
    const response = await fetch('https://localhost:7102/jwt');
    token = await response.text();
}

fetchToken().then(() => {
    console.log(token); 
});

console.log("TOKEN: " +  token );
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub",
    {
        accessTokenFactory: () => token
    })
    .withAutomaticReconnect().build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} says ${message}`;
});

connection.on("ReceiveMessage2", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `UNVERIFIED Ted-${user} says ${message}`;
});

connection.on("CreateToken", function (token) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${token}`;
});

connection.onreconnected(function(connectionId){console.log(`Reconnected at ${connectionId}`)})

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("sendButtonTED").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendPrivateMessage", "TEDGROUP",  user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("joinTedGroupButton").addEventListener("click", function(event){
    connection.invoke("AddToGroup", "TEDGROUP").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
})

document.getElementById("createTokenButton").addEventListener("click", function (event) {
    connection.invoke("CreateToken").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
})