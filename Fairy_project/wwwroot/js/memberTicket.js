
//ToDo:重構 分類 


//render member's tickets
const ticketList = []
const carouselInner = document.querySelector('.carousel-inner')

axios.post('/api/Member/Post/getTicketsss', { "Mf_id": 6 })
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
    for (let i = 0; i < ticketList.length; i++) {
        if (i == 0) {
            pannelHTML += `
            <div class="carousel-item active">
                <div class=" d-flex w-auto">
                    <div class="btn-group-vertical">
                        <button type="button" class="btn-size-top btn-qrcode" data-exid="${ticketList[i].exhibition.exhibitId}" data-order="${ticketList[i].ticket.orderNum}" data-bs-toggle="modal" data-bs-target="#QRcode_Modal">QRCODE</button>
                        <div style="background-color:#DDCFC2 ;width:196px;height:4px;"></div>
                        <button type="button" class="btn-size-bottom btn-give" data-order="${ticketList[i].ticket.orderNum}" data-bs-toggle="modal" data-bs-target="#give_Modal">分票</button>
                    </div>
                    <img src="${ticketList[i].exhibition.exhibitTImg}" class="d-block w-100-h-setting" alt="...">
                </div>
            </div>
            `
        } else {
            pannelHTML += `
                <div class="carousel-item">
                    <div class=" d-flex w-auto">
                        <div class="btn-group-vertical">
                            <button type="button" class="btn-size-top btn-qrcode" data-exid="${ticketList[i].exhibition.exhibitId}" data-order="${ticketList[i].ticket.orderNum}" data-bs-toggle="modal" data-bs-target="#QRcode_Modal">QRCODE</button>
                            <div style="background-color:#DDCFC2 ;width:196px;height:4px;"></div>
                            <button type="button" class="btn-size-bottom btn-give" data-order="${ticketList[i].ticket.orderNum}" data-bs-toggle="modal" data-bs-target="#give_Modal">分票</button>
                        </div>
                        <img src="${ticketList[i].exhibition.exhibitTImg}" class="d-block w-100-h-setting" alt="Ticket Image">
                    </div>
                </div>
                `
        }
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
                width: 400,
                height: 400,
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