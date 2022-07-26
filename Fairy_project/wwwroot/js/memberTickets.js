const ticketList = []
axios.post('/api/Member/Post/getTicketsss', { "Mf_id": 1 })
    .then(res => {
        console.log(res.data)
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
    const carouselInner = document.querySelector('.carousel-inner')
    let pannelHTML = ``;
    console.log(ticketList)
    //ticketList.forEach(tobj => {
    //    pannelHTML += `
    //    <div class="carousel-item">
    //    <div class=" d-flex w-auto">
    //        <div class="btn-group-vertical">
    //            <button type="button" class="btn-size-top" data-ExId="${tobj.exhibition.exhibitId}">QRCODE</button>
    //            <div style="background-color:#DDCFC2 ;width:196px;height:4px;"></div>
    //            <button type="button" class="btn-size-bottom" data-orderNum="${tobj.ticket.orderNum}">分票</button>

    //        </div>
    //        <img src="${tobj.exhibition.exhibit_T_img}" class="d-block w-100-h-setting" alt="...">
    //    </div>
    //</div>
    //`})
    //carouselInner.innerHTML = pannelHTML
}