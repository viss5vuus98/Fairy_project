const btnCart = document.querySelector('.btn_cart')
const cartList = JSON.parse(sessionStorage.getItem('TicketList')) || []
btnCart.addEventListener('click', event => {
    const data = event.target.dataset
    const exhibition = {
        id: data.id,
        count: 1,
    }
    console.log(exhibition)
    if (cartList.length == 0) {
        cartList.push(exhibition)
        sessionStorage.setItem('TicketList', JSON.stringify(cartList))
    } else if (!cartList.filter(obj => obj.id == exhibition.id).length > 0) {
        cartList.push(exhibition)
        sessionStorage.setItem('TicketList', JSON.stringify(cartList))
    } else {
        alert('已經加入購物車了')
    }
    //if (cartList.find(item => item.id == exhibition.id).length >= 1) {
    //    cartList.push(exhibition)
    //    sessionStorage.setItem('TicketList', JSON.stringify(cartList))
    //} else if (cartList.length == 0) {
    //    cartList.push(exhibition)
    //    sessionStorage.setItem('TicketList', JSON.stringify(cartList))

    //} else {
    //    alert('已經加入購物車了')
    //}
})

function renderBooths(exhibitId) {
    axios.post(rootUrl + 'GetInvideManufactures', { "EX_id": exhibitId })
        .then(res => console.log(res.data))
        .catch(err => console.log(err))
}

renderBooths(exhibitId)


