/* Vi starter her ved å sette opp en ny connection mot vår chathub. */
const connection = new signalR.HubConnectionBuilder()
  .withUrl("/chatHub")
  .build();

/* Vi setter også opp en referanse til channelName vi kan bruke, istedenfor å alltid lese html.*/
let channelName = "";
let loggedIn = false;

let activeChannels = await fetchChannelNames();
setInterval(async () => {
  const channelNameList = document.getElementById("channelNames");
  const newList = document.createElement("ul");
  newList.id = "channelNames";
  activeChannels = await fetchChannelNames();
  activeChannels.forEach((channel) => {
    const li = document.createElement("li");
    li.textContent = channel;
    li.classList.add("activeChannel");
    newList.appendChild(li);
  });
  const channelNameArea = document.getElementById("channelList");
  channelNameList.remove();
  channelNameArea.append(newList);
}, 10000);

let hubError = null;

const recievedMessageCssClass = "server-message";
const sentUserMessageCssClass = "user-message";

/* siden vår js fil er en modul, og blir defer loaded, må vi hente inn,
og sette eventlisteners etter html er loaded. */
document.getElementById("joinBtn").addEventListener("click", joinChannel);
document.getElementById("sendBtn").addEventListener("click", sendMessage);
document.getElementById("createBtn").addEventListener("click", createChannel);
document.getElementById("signUpBtn").addEventListener("click", signUp);
document.getElementById("logInBtn").addEventListener("click", logIn);
document.getElementById("logOutBtn").addEventListener("click", logOut);


/* Her setter vi opp en lytter mot ReceiveMessage eventen vi tilgjengeliggjør i hubben vår. */
connection.on("ReceiveMessage", (user, message) => {
  let messageCssClass = recievedMessageCssClass;
  if (user === document.getElementById("user").value) {
    messageCssClass = sentUserMessageCssClass;
  }
  printMessage(user, message, messageCssClass);
});

/* Her definerer vi en asynkron funksjon, som invoker vår JoinChannel method på hubben på serveren vår. */
async function joinChannel() {
  const channel = document.getElementById("channel").value;
  const username = document.getElementById("user").value;
  if (channelName !== "" && !hubError) {
    await connection.invoke("LeaveChannel", channelName, username);
    hubError = null;
  }
  channelName = channel;
  try {
    const list = document.getElementById("messageList");
    console.log(list);
    list.childNodes.forEach((child) => child.remove());
    await connection.invoke("JoinChannel", channel, username);
    document.getElementById("channelOutput").textContent = channelName;
    hubError = null;
  } catch (err) {
    hubError = err;
  }
  /* Vi legger også channelnavnet inn i channelOutput*/
}

async function fetchChannelNames() {
  const response = await fetch("api/HubInformation/channels");
  return await response.json();
}

async function createChannel() {
  const channel = document.getElementById("channel").value;
  const username = document.getElementById("user").value;
  if (channelName !== "" && !hubError) {
    await connection.invoke("LeaveChannel", channelName, username);
    hubError = null;
  }
  channelName = channel;
  try {
    const list = document.getElementById("messageList");
    list.childNodes.forEach((child) => child.remove());
    await connection.invoke("CreateChannel", channel, username);
    document.getElementById("channelOutput").textContent = channelName;
    hubError = null;
  } catch (err) {
    hubError = err;
  }
  /* Vi legger også channelnavnet inn i channelOutput*/
}

async function logIn() {
  const userName = document.getElementById("user").value;
  if (
    userName === "" ||
    userName === null ||
    userName === undefined ||
    userName.trim().length === 0
  ) {
    return;
  }
  const password = document.getElementById("password").value;
  if (
      password === "" ||
      password === null ||
      password === undefined ||
      userName.trim().length === 0
  ) return;
  const requestOptions = {
    method: "POST",
      headers: {"Content-Type": "application/json"},
      body: JSON.stringify({userName: userName, password: password}),
  };
  const response = await fetch(
    `api/HubInformation/users/logIn`,
    requestOptions
  );
  console.log(response);
  if (response.ok) {
    document.getElementById("user").disabled = true;
    document.getElementById("password").disabled = true;
    document.getElementById("signUpBtn").disabled = true;
    document.getElementById("logInBtn").disabled = true;
    loggedIn = true;
  }
}

async function signUp() {
  const userName = document.getElementById("user").value;
  if (
    userName === "" ||
    userName === null ||
    userName === undefined ||
    userName.trim().length === 0
  ) {
    return;
  }
    const password = document.getElementById("password").value;
    if (
        password === "" ||
        password === null ||
        password === undefined ||
        userName.trim().length === 0
    ) return;
    const requestOptions = {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({userName: userName, password: password}),
    };
  const response = await fetch(
    `api/HubInformation/users/signUp`,
    requestOptions
  );
  console.log(response);
  if (response.ok) {
    document.getElementById("user").disabled = true;
    document.getElementById("password").disabled = true;
    document.getElementById("signUpBtn").disabled = true;
    document.getElementById("logInBtn").disabled = true;
    loggedIn = true;
  }
}

async function logOut() {
    const userName = document.getElementById("user").value;
    if (
        userName === "" ||
        userName === null ||
        userName === undefined ||
        userName.trim().length === 0
    ) {
        return;
    }
    const requestOptions = {
        method: "POST",
    };
    const response = await fetch(
        `api/HubInformation/users/signup/${userName}`,
        requestOptions
    );
    
    if (response.ok) {
        document.getElementById("user").disabled = false;
        document.getElementById("password").disabled = false;
        document.getElementById("user").textContent = "";
        document.getElementById("password").textContent = "";
        document.getElementById("signUpBtn").disabled = false;
        document.getElementById("logInBtn").disabled = false;
        loggedIn = false;
    }
}

/* Dette er en hjelpefunksjon som printer en melding til message listen vår.*/
function printMessage(user, message, className) {
  const li = document.createElement("li");
  li.textContent = `${user} ${message}`;
  li.classList.add(className);
  document.getElementById("messageList").appendChild(li);
}

/* Dette er en asynkron funksjon som sender en melding til vår chathub, ved å invoke SendMessageToChannel metoden i vår chathub.*/
async function sendMessage() {
    if (!loggedIn) return;
  if (
    channelName === "" ||
    channelName === null ||
    channelName === undefined ||
    channelName.trim().length === 0
  )
    return;
  const userName = document.getElementById("user").value;
  if (
    userName === "" ||
    userName === null ||
    userName === undefined ||
    userName.trim().length === 0
  ) {
    return;
  }
  const message = document.getElementById("message").value;
  printMessage(userName, message, sentUserMessageCssClass);
  await connection.invoke(
    "SendMessageToChannel",
    channelName,
    userName,
    message
  );
}
connection.start();
