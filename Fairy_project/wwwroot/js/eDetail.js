const btnCart = document.querySelector('.btnCart')

btnCart.addEventListener('click', event => {
    const data = event.target.dataset
    console.log(data)
})