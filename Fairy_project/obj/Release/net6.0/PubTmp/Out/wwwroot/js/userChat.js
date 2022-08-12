// 顯示Chat視窗//

const chatCircle = document.getElementById('chat-circle')
const chatCard = document.getElementById('chat-card')
const chatExitBtn = document.getElementById('chat-exit-btn')

chatCircle.addEventListener('click', event => {
    chatCard.classList.toggle("scaleBox")
    chatCircle.classList.toggle("scaleBox")
})

chatExitBtn.addEventListener('click', event => {
    chatCard.classList.toggle("scaleBox")
    chatCircle.classList.toggle("scaleBox")
})


const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").withAutomaticReconnect().build()
const acount = sessionStorage.getItem("acount")
console.log(acount)
//send Message//
window.addEventListener('load', () => {
    document.querySelector('#send-msg').addEventListener('click', sendMsg)
    document.querySelector('#input-msg').addEventListener('keydown', function (e) {
        if (e.keyCode === 13) {
            sendMsg(e)
        }
    }, false)
})
function sendMsg(event) {
    event.preventDefault();
    const message = document.querySelector('#input-msg').value
    if (message.trim().length === 0) {
        return
    }
    console.log(message)
    //送出訊息至server
    //start存入ID
    
    connection.invoke("UserSendMessage", message, acount).catch(function (err) {
        console.log("錯誤" + err.toString())
    })
    document.querySelector('#input-msg').value = ''
}

connection.start().then(function () {
    console.log("Hub 連線完成")
}).catch(function (err) {
    console.error(err)
})


//使用者發話 (自己顯示)//
connection.on("userPostover", function (message) {
    console.log(message)
    const ChatDiv = document.createElement("div")
    ChatDiv.classList.add("d-flex", "align-items-baseline", "text-end", "justify-content-end", "mb-4")
    ChatDiv.innerHTML = `
                <div class="pe-2">
                    <div>
                        <div class="card card-text d-inline-block p-2 m-1 right-card-text">${message}</div>
                    </div>
                </div>

                <div class="position-relative avatar">
                    <i class="fa-solid fa-user-secret people"></i>
                </div>
        `
    const chatContent = document.getElementById('chat-content')
    chatContent.appendChild(ChatDiv)
    chatContent.scrollTo({
        top: 2000,
        behavior: "smooth"
    });

})

//使用者接收//
connection.on("userTakeover", function (message) {
    const ChatDiv = document.createElement("div")
    ChatDiv.classList.add("d-flex", "align-items-baseline", "mb-4")
    ChatDiv.innerHTML = `
            <div class="position-relative avatar">
                <i class="fa-solid fa-user-astronaut people"></i>
            </div>
                <!-- 對話框 -->
            <div class="pe-2">
                <div>
                    <div class="card card-text d-inline-block p-2 m-1 left-card-text">${message}</div>
                    <div class="small">01:13PM</div>
                </div>
            </div>
        `
    document.getElementById('chat-content').appendChild(ChatDiv)
})


//connection.on("UpdList", function (jsonList) {
//    const list = JSON.parse(jsonList);
//    for (let i = 0; i < list.length; i++) {
//        console.log(list[i])
//    }
//})
