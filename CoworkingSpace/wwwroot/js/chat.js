// The following sample code uses modern ECMAScript 6 features 
// that aren't supported in Internet Explorer 11.
// To convert the sample for environments that do not support ECMAScript 6, 
// such as Internet Explorer 11, use a transpiler such as 
// Babel at http://babeljs.io/. 
//
// See Es5-chat.js for a Babel transpiled version of the following code:

var connectionId = "";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .configureLogging(signalR.LogLevel.Trace)
    .build();

connection.on("ReceiveChatMessage", (message, user) => {
    const msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    const encodedMsg = user + " says: " + msg;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("ReceiveDirectMessage", (message, user) => {
    const msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    const encodedMsg = user + " privately says: " + msg;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("ReceiveGroupMessage", (message, user) => {
    const msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    const encodedMsg = user + " privately group says: " + msg;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("ReceiveSystemMessage", (message, user) => {
    const msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    const encodedMsg = 'system says: ' + user + ' ' + msg;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
})
    .then(() => console.error("Connection established!!!"))
    .catch(err => console.error(err.toString()));

document.getElementById("sendButton").addEventListener("click", event => {
    const message = document.getElementById("messageInput").value;

    var userName = $("form ul li a")[0].innerHTML;
    var offset = userName.lastIndexOf(" ");
    var groupName = userName.slice(offset + 1).split("!")[0];

    connection.invoke("AddToGroup", groupName).catch(err => console.error(err.toString()));
    connection.invoke("SendMessageToGroup", groupName, message).catch(err => console.error(err.toString()));

    event.preventDefault();
});

document.getElementById("collapse").addEventListener("click", event => {

    if (parseInt(document.getElementById("chat").style.height) > 100) {
        document.getElementById("chat").style.height = "40px";
    }
    else {
        document.getElementById("chat").style.height = "200px";
    }
});




