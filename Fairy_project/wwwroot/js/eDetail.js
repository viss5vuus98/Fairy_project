const btnCart = document.querySelector('.btn_cart')
const cartList = JSON.parse(sessionStorage.getItem('TicketList')) || []
const content = document.getElementById('cards-content')
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
        alert('加入購物車!')
    } else if (!cartList.filter(obj => obj.id == exhibition.id).length > 0) {
        cartList.push(exhibition)
        sessionStorage.setItem('TicketList', JSON.stringify(cartList))
        alert('加入購物車!')
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
getBooths(exhibitId)
function getBooths(exhibitId) {
    axios.post(rootUrl + 'GetInvideManufactures', { "EX_id": exhibitId })
        .then(res => {
            console.log(res.data)
            renderBooths(res.data)
        })
        .catch(err => console.log(err))
}

function renderBooths(data) {
    const content = document.querySelector('.card_content')
    let innerContent = `<div class="col-lg-1"></div>`
    for (let i = 0; i <= 3; i++) {
        innerContent += `
               <div class="col-lg-3 col-sm-8">
                <figure class="wow fadeInLeft animated animated" data-wow-duration="500ms" data-wow-delay="300ms"
                        style="visibility: visible; animation-duration: 500ms; animation-delay: 300ms; animation-name: fadeInLeft;">
                    <div class="img-wrapper">
                        <img src="${data[i].boothMapss.mfLogo}" class="img-responsive" alt="this is a title">
                    </div>
                    <figcaption>
                        <h4>
                            <a href="#">
                                ${data[i].Manufacturess.mfLogo}
                            </a>
                        </h4>
                        <p>
                            Lorem ipsum dolor sit.
                        </p>
                    </figcaption>
                </figure>
              </div>
        `
    }
}


axios.post('')