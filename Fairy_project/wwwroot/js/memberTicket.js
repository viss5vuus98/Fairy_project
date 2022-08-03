
//ToDo:重構 分類 


//render member's tickets
const ticketList = []
const memberId = JSON.parse(sessionStorage.getItem("Info")).id
const carouselInner = document.querySelector('.carousel-inner')


axios.post('/api/Member/Post/getTicketsss', { "Mf_id": memberId})
    .then(res => {
        
        for (let i = 0; i < res.data.tickets.length; i++) {
            const ticketobj = new Object()
            ticketobj.ticket = res.data.tickets[i]
            ticketobj.exhibition = res.data.exhibition[i]
            ticketList.push(ticketobj)
        }
        renderCards(ticketList)    
    })
    .catch(err =>
        console.log(err)
    )
function renderCards(ticketList) {
    let pannelHTML = '';
    if (ticketList.length > 0) {
        for (let i = 0; i < ticketList.length; i++) {
                pannelHTML += `
                    < div class="ticket-container" >
                        <div class="card-img">
                            <img src="${root}${ticketList[i].exhibition.exhibitTImg}" alt="Ticket Image" class="ticket_card_img">
                                    </div>
                            <div class="ticket_card_body">
                                <h4>${ticketList[i].exhibition.exhibitName}</h4>
                                <div class="ticket_date">
                                    <span>${ticketList[i].exhibition.datefrom}</span> ~ <span>${ticketList[i].exhibition.dateto}</span>
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
        }       
    } else {
        pannelHTML += '<div>您還沒有購買票券</div>'
    }
    carouselInner.innerHTML = pannelHTML    
}



carouselInner.addEventListener('click', event => {
    const target = event.target
    if (target.matches('.btn-qrcode')) {
        showQRcodeModal(target.dataset.exid, target.dataset.order)
    } else if (target.matches('.btn-give')) {
        giveTicket(target.dataset.order)
    }
})

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

function giveTicket(orderNum) {
    const giveOrder = document.querySelector('.give_order')
    giveOrder.value = orderNum
}

const submit = document.querySelector('.submit')

submit.addEventListener('click', e => {
    e.preventDefault()
    const email = document.querySelector('#give_modal_email').value
    const order = Number(document.querySelector('.give_order').value)
    axios.post('/api/Member/Post/giveTicket', { "eamil": email, "order": order })
        .then(res => console.log(res.data))
        .catch(err => console.log(err))
})