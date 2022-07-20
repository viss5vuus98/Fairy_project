const btnCart = document.querySelector('.btn_cart')
const cartList = JSON.parse(sessionStorage.getItem('tickets')) || []
btnCart.addEventListener('click', event => {
    const data = event.target.dataset
    const product = {}
    product.id = data.id
    if (!cartList.includes(data.id)) {
        cartList.push(product)
        sessionStorage.setItem('tickets', JSON.stringify(cartList))
    } else {
        alert('已經加入購物車了')
    }
})