const btnCart = document.querySelector('.btn_cart')
const cartList = JSON.parse(sessionStorage.getItem('Tickets')) || []
btnCart.addEventListener('click', event => {
    const data = event.target.dataset
    console.log(cartList)
    if (!cartList.includes(data.id)) {
        cartList.push(data.id)
        JSON.stringify(sessionStorage.setItem('Tickets', 'carList'))
    } else {
        alert('已經加入購物車了')
    }
})