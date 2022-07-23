const btnCart = document.querySelector('.btn_cart')
const cartList = JSON.parse(sessionStorage.getItem('TicketList')) || []
btnCart.addEventListener('click', event => {
    const data = event.target.dataset
    console.log(cartList)
    console.log(data.id)
    if (!cartList.includes(data.id)) {
        cartList.push(data.id)
        sessionStorage.setItem('TicketList', JSON.stringify(cartList))
    } else {
        alert('已經加入購物車了')
    }
})
