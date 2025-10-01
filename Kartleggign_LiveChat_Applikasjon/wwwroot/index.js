
/* Vi starter her ved å sette opp en ny connection mot vår chathub. */
const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

/* Vi setter også opp en referanse til channelName vi kan bruke, istedenfor å alltid lese html.*/
let channelName = "";

let activeChannels = await fetchChannelNames();

setInterval(async ()=>{
    activeChannels = await fetchChannelNames();
    console.log(activeChannels);
}, 10000)

let hubError = null;

/* siden vår js fil er en modul, og blir defer loaded, må vi hente inn,
og sette eventlisteners etter html er loaded. */
document.getElementById("joinBtn").addEventListener("click", joinChannel);
document.getElementById("sendBtn").addEventListener("click", sendMessage);
document.getElementById("createBtn").addEventListener("click", createChannel);
document.getElementById("signUpBtn").addEventListener("click", signUp);
document.getElementById("logInBtn").addEventListener("click", logIn);

/* Her setter vi opp en lytter mot ReceiveMessage eventen vi tilgjengeliggjør i hubben vår. */
connection.on("ReceiveMessage", (user, message) => {
    printMessage(user, message)
})

/* Her definerer vi en asynkron funksjon, som invoker vår JoinChannel method på hubben på serveren vår. */
async function joinChannel() {
    const channel = document.getElementById("channel").value;
    const username = document.getElementById("user").value;
    if (channelName !== ""&& !hubError) {
        await connection.invoke("LeaveChannel", channelName, username);
        hubError = null;
    }
    channelName = channel;
    try {
        await connection.invoke("JoinChannel", channel, username);
        document.getElementById("channelOutput").textContent = channelName;
        hubError = null;
    } catch (err) {
        hubError = err;
    }
    /* Vi legger også channelnavnet inn i channelOutput*/
}

async function fetchChannelNames(){
    const response = await fetch("api/HubInformation/channels");
    return await response.json();
}

async function createChannel(){
    const channel = document.getElementById("channel").value;
    const username = document.getElementById("user").value;
    if (channelName !== "" && !hubError)
    {
        await connection.invoke("LeaveChannel", channelName, username);
        hubError = null;
    }
    channelName = channel;
    try{
        await connection.invoke("CreateChannel", channel, username);
        document.getElementById("channelOutput").textContent = channelName;
        hubError = null;
    } catch (err) {
        hubError = err;
    }
    /* Vi legger også channelnavnet inn i channelOutput*/
}

async function logIn()
{
    const userName = document.getElementById("user").value;
    if (userName === "" || userName === null || userName === undefined || userName.trim().length === 0){
        return;
    }
    const requestOptions = {
        method: "POST",
    }
    const response = await fetch(`api/HubInformation/users/logIn?userName=${userName}`, requestOptions)
    if (response.ok){
        document.getElementById("user").disabled = true;
        document.getElementById("logInBtn").disabled = true;
    }
}

async function signUp()
{
    const userName = document.getElementById("user").value;
    if (userName === "" || userName === null || userName === undefined || userName.trim().length === 0){
        return;
    }
    const requestOptions = {
        method: "POST",
    }
    const response = await fetch(`api/HubInformation/users/signup?userName=${userName}`, requestOptions)
    if (response.ok){
        document.getElementById("user").disabled = true;
        document.getElementById("signUpBtn").disabled = true;
    }
}

/* Dette er en hjelpefunksjon som printer en melding til message listen vår.*/
function printMessage(user, message) {
    const li = document.createElement("li");
    li.textContent = `${user}: ${message}`;
    document.getElementById("messageList").appendChild(li);
}

/* Dette er en asynkron funksjon som sender en melding til vår chathub, ved å invoke SendMessageToChannel metoden i vår chathub.*/
async function sendMessage() {
    const channel = channelName.length === 0 ? document.getElementById("channel").value : channelName;
    const message = document.getElementById("message").value;
    const user = document.getElementById("user").value;
    printMessage(user, message);
    await connection.invoke("SendMessageToChannel", channel, user, message );
}
connection.start();