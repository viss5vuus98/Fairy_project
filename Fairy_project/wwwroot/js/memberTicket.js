
//ToDo:重構 分類

const ticketContents = document.querySelectorAll('.ticket-content');
const leftContent = document.getElementById('ticket-list');
const exhibitList = [];
const memberId = JSON.parse(sessionStorage.getItem("Info")).id
const carouselInner = document.querySelector('.tickets-pannal')
//render member's tickets





const view = {
    renderCardList(cardList){
        let panelHTML = ''
        for (let i = 0; i < cardList.length; i++) {
            panelHTML += `
                <div class="card">
                    <p class="name" data-exid="${cardList[i].exhibitId}">${cardList[i].exhibitName}</p>
                </div>
            `
        }
        document.getElementById('ticket-list').innerHTML += panelHTML
    },
    renderPanelOnload(tickets, exhibition) {
        let panelHTML = ``
        const dateStart = new Date(exhibition.datefrom)
        const dateEnd = new Date(exhibition.dateto)
        for (let i = 0; i < tickets.length; i++) {
            panelHTML += `
              <div class="ticket-content mx-auto">
                <div class="flip-ticket">
                    <div class="ticket-front">
                        <div class="ticket-img">
                            <img src="${root}${exhibition.exhibitTImg}" alt="">
                        </div>
                    </div>
                    <div class="ticket-back">
                        <div class="back-left">
                            <div class="ticket-back-img">
                                <img src="${root}${exhibition.exhibitPImg}" alt="ticket" class="back-img">
                                <p class="admit-one">
                                    <span>ADMIT ONE</span>
                                    <span>單人入場</span>
                                    <span>ADMIT ONE</span>
                                </p>
                            </div>
                            <div class="ticket-info">
                                <div class="ticket-date">
                                    <span class="year">${dateStart.getFullYear()}</span>
                                    <span class="month">${dateStart.getMonth()}</span>
                                    <span>月</span>
                                    <span class="month">${dateStart.getDate()}</span>
                                    <span>日</span>
                                    <span>~</span>
                                    <span class="month">${dateEnd.getMonth()}</span>
                                    <span>月</span>
                                    <span class="month">${dateEnd.getDate()}</span>
                                    <span>日</span>
                                </div>
                                <div class="ticket-body">
                                    <h3 class="ticket-title">${exhibition.exhibitName}</h3>
                                    <div class="time">
                                        <span class="time-open">9:00 AM</span>
                                        <span>To</span>
                                        <span class="time-close">5:00 PM</span>
                                    </div>
                                    <div class="area">
                                        <span>請至</span>
                                        <span>${exhibition.areaNum}館</span>
                                        <span>入場</span>
                                    </div>
                                </div>
                                <div class="ticket-footer">
                                    <span>高雄市前金區中正四路211號</span>
                                </div>
                            </div>
                        </div>
                        <div class="back-right">
                            <p class="admit-one right">
                                <span>ADMIT ONE</span>
                                <span>單人入場</span>
                                <span>ADMIT ONE</span>
                            </p>
                            <div class="btn-section">
                                <div class="btn-give give" data-exid="${exhibition.exhibitId}" data-order="${tickets[i].orderNum}" data-bs-toggle="modal"
                                     data-bs-target="#give_Modal">
                                    <span class="info--text give" data-exid="${exhibition.exhibitId}" data-order="${tickets[i].orderNum}">贈票</span>
                                    <div class="icon give" data-exid="${exhibition.exhibitId}" data-order="${tickets[i].orderNum}">
                                        <i class="fa-solid fa-gift fa-xl give" data-exid="${exhibition.exhibitId}" data-order="${tickets[i].orderNum}"></i>
                                    </div>
                                </div>
                                <div class="btn-qrcode qr" data-exid="${exhibition.exhibitId}"
                                     data-order="${tickets[i].orderNum}" data-bs-toggle="modal" data-bs-target="#QRcode_Modal">
                                    <span class="info--text qr"  data-exid="${exhibition.exhibitId}"
                                     data-order="${tickets[i].orderNum}">掃碼</span>
                                    <div class="icon qr" data-exid="${exhibition.exhibitId}"
                                     data-order="${tickets[i].orderNum}">
                                        <i class="fa-solid fa-qrcode fa-xl qr"  data-exid="${exhibition.exhibitId}"
                                     data-order="${tickets[i].orderNum}"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            `
        }
        carouselInner.innerHTML = panelHTML
        this.dealtickets(document.querySelectorAll('.ticket-content'))
    },
    dealtickets(ticketContents) {
        for (let i = 0; i < ticketContents.length; i++) {
            setTimeout(() => {
                ticketContents[i].classList.add("dealCards")
            }, 75 * (i + 10))            
        }
    }
}
const model = {    
    getCardList(ticketList) {
        const firstIndex = ticketList.exhibition[0].exhibitId
        const tickets = ticketList.tickets.filter((ticket) => {
            return ticket.eId === firstIndex
        })
        view.renderCardList(ticketList.exhibition)
        view.renderPanelOnload(tickets, ticketList.exhibition[0])
    },
    getTicket(exid, memberId) {
        const exhibition = exhibitList.find(exhibit => exhibit.exhibitId === exid)
        axios.post('/api/Member/Post/getTicketOfExhibit', { "Ex_id": exid, "Mf_id": memberId })
            .then(res => {
                view.renderPanelOnload(res.data, exhibition)
            })
            .catch(err => {
                console.log(err)
            })
    }
}


function renderCards(ticketList) {
    let pannelHTML = ``;
    if (ticketList.length > 0) {
        for (let i = 0; i < ticketList.length; i++) {
            const dateStart = new Date(ticketList[i].exhibition.datefrom).toLocaleDateString()
            const dataEnd = new Date(ticketList[i].exhibition.dateto).toLocaleDateString()
                pannelHTML += `
                    <div class="ticket-container">
                        <div class="card-img">
                            <img src="${root}${ticketList[i].exhibition.exhibitTImg}" alt="Ticket Image" class="ticket_card_img">
                        </div>
                        <div class="ticket_card_body">
                            <h4>${ticketList[i].exhibition.exhibitName}</h4>
                            <div class="ticket_date">
                                <span>${dateStart}</span> ~ <span>${dataEnd}</span>
                            </div>
                            <div class="ticket_status">
                                開始中
                            </div>
                            <div class="btn_section">
                                <button type="button" class="btn btn-dark btn-qrcode" data-exid="${ticketList[i].exhibition.exhibitId}" data-order="${ticketList[i].ticket.orderNum}" data-bs-toggle="modal" data-bs-target="#QRcode_Modal">入場</button>
                                <button type="button" class="btn btn-dark btn-give" data-order="${ticketList[i].ticket.orderNum}" data-bs-toggle="modal" data-bs-target="#give_Modal">贈票</button>
                            </div>
                        </div>                        
                    </div>
                `


            pannelHTML +=`
                <div class="ticket-content mx-auto">
                <div class="flip-ticket">
                    <div class="ticket-front">
                        <div class="ticket-img">
                            <img src="~/images/T02.jpg" alt="">
                        </div>
                    </div>
                    <div class="ticket-back">
                        <div class="back-left">
                            <div class="ticket-back-img">
                                <img src="~/images/P02.jpg" alt="ticket" class="back-img">
                                <p class="admit-one">
                                    <span>ADMIT ONE</span>
                                    <span>單人入場</span>
                                    <span>ADMIT ONE</span>
                                </p>
                            </div>
                            <div class="ticket-info">
                                <div class="ticket-date">
                                    <span class="year">2022</span>
                                    <span class="month">8</span>
                                    <span>月</span>
                                    <span class="month">19</span>
                                    <span>日</span>
                                    <span>~</span>
                                    <span class="month">8</span>
                                    <span>月</span>
                                    <span class="month">21</span>
                                    <span>日</span>
                                </div>
                                <div class="ticket-body">
                                    <h3 class="ticket-title">高雄國際食品展覽</h3>
                                    <div class="time">
                                        <span class="time-open">9:00 AM</span>
                                        <span>To</span>
                                        <span class="time-close">5:00 PM</span>
                                    </div>
                                    <div class="area">
                                        <span>請至</span>
                                        <span>A館</span>
                                        <span>入場</span>
                                    </div>
                                </div>
                                <div class="ticket-footer">
                                    <span>高雄市前金區中正四路211號</span>
                                </div>
                            </div>
                        </div>
                        <div class="back-right">
                            <p class="admit-one right">
                                <span>ADMIT ONE</span>
                                <span>單人入場</span>
                                <span>ADMIT ONE</span>
                            </p>
                            <div class="btn-section">
                                <div class="btn-give" data-order="1" data-bs-toggle="modal"
                                     data-bs-target="#give_Modal">
                                    <span class="info--text">贈票</span>
                                    <div class="icon">
                                        <ion-icon name="add-outline"><i class="fa-solid fa-gift fa-xl"></i></ion-icon>
                                    </div>
                                </div>
                                <div class="btn-qrcode" data-exid="1"
                                     data-order="1" data-bs-toggle="modal" data-bs-target="#QRcode_Modal">
                                    <span class="info--text">掃碼</span>
                                    <div class="icon">
                                        <ion-icon name="add-outline"><i class="fa-solid fa-qrcode fa-xl"></i></ion-icon>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            `
        }       
    } else {
        pannelHTML += '<div>您還沒有購買票券</div>'
    }
    carouselInner.innerHTML = pannelHTML    
}



carouselInner.addEventListener('click', event => {
    
    const target = event.target
    console.log(target)
        if (target.matches('.btn-qrcode')) {
            showQRcodeModal(target.dataset.exid, target.dataset.order)
        } else if (target.matches('.give')) {
            console.log(target.dataset.order)
            giveTicket(target.dataset.exid ,target.dataset.order)
        }
    })

    leftContent.addEventListener('click', event => {
        if (event.target.classList.contains("name")) {
            const cards = document.querySelectorAll('.card')
            for (let i = 0; i < cards.length; i++) {
                cards[i].classList.remove('target')
            }
            event.target.parentElement.classList.add('target')

            model.getTicket(Number(event.target.dataset.exid), memberId)          
        }
    })

//getdata

    axios.post('/api/Member/Post/getTicketsss', { "Mf_id": memberId })
        .then(res => {
            console.log(res.data)
            model.getCardList(res.data)
            exhibitList.push(...res.data.exhibition)
        })
        .catch(err =>
            console.log(err)
        )



    





//render QRcode modal


function showQRcodeModal(id, order) {
    const QRTtitle = document.querySelector('#QRModal-title');
    const QRBody = document.querySelector('#QR-body');
    axios.post('/api/Member/Post/getVerificationCode', { "Ex_id": id, "Mf_id": order})
        .then(res => {
            const exhibition = res.data
            QRTtitle.textContent = exhibition.exhibitName;
            QRBody.innerHTML = '<div id="QRcode"></div>'
            //new QRCode(document.getElementById("QRcode"), exhibition.verificationCode);
            new QRCode(document.getElementById("QRcode"), {
                text: exhibition.verificationCode,
                width: 300,
                height: 300,
                colorDark: "#000000",
                colorLight: "#ffffff",
                correctLevel: QRCode.CorrectLevel.H
            });
          
        })
        .catch(err => console.log(err))
}

// give ticket

function giveTicket(exid, orderNum) {
    const giveOrder = document.querySelector('#give_order')
    giveOrder.value = orderNum
    document.querySelector('.submit').setAttribute('data-exid', exid)
}

const submit = document.querySelector('.submit')

submit.addEventListener('click', e => {
    e.preventDefault()
    const email = document.querySelector('#give_modal_email').value
    const order = Number(document.querySelector('#give_order').value)
    if (email.trim().length > 0 && document.querySelector('#give_order').value.trim().length > 0) {
        axios.post('/api/Member/Post/giveTicket', { "eamil": email, "order": order })
            .then(res => {
                model.getTicket(id, memberId)
                alert('成功寄送')
            })
            .catch(err => console.log(err))
    } else {
        alert('輸入錯誤')
    }
    
})

