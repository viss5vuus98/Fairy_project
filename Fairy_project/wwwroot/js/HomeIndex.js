//Render Exhibitions

const VModel = {
    getData() {
        axios.get('/Home/getAllExhibition')
            .then(res => {
                const exhibitionList = []
                exhibitionList.push(...res.data)
                View.renderExhibitions(exhibitionList)
                View.renderCarousel(exhibitionList)
            })
            .catch(err => console.log(err))
    }
}

const View = {
    renderExhibitions(data) {
        const E_Content = document.getElementById('exhibitions_content')
        let innerContent = ''
        for (let i = 0; i < data.length; i++) {
            innerContent += `
                <div class="col-md-4 col-sm-6">
                <figure class="wow fadeInLeft animated portfolio-item" data-wow-duration="500ms" data-wow-delay="0ms">
                    <div class="img-wrapper">
                        <img src="${root}${data[i].exhibitPImg}" class="img-fluid" alt="this is a title">
                        <div class="overlay">
                            <div class="buttons">
                                <a rel="gallery" class="fancybox" href="images/portfolio/item-1.jpg">Demo</a>
                                <a target="" href="Home/exhibitionDetail/${data[i].exhibitId}">Details</a>
                            </div>
                        </div>
                    </div>
                    <figcaption>
                        <h4 class="card_title">
                            <a href="#">
                                ${data[i].exhibitName}
                            </a>
                        </h4>
                        <p class="card_description">
                            ${data[i].exDescription}
                        </p>
                    </figcaption>
                </figure>
            </div>
            `
        }
        E_Content.innerHTML = innerContent
        View.getCardTextLength()
    },
    getCardTextLength() {
        const cards = document.querySelectorAll('.card_description')
        const maxLength = 50
        cards.forEach(card => {
            const length = card.innerText.length
            if (length > maxLength) {
                let moddifyText = card.innerText.slice(0, maxLength) + '...'
                card.innerText = moddifyText
            }

        })
    },
    renderCarousel(exhibitionList) {
        const carouselList = []
        const carousel = document.querySelector('.carousel-inner')
        let index = Math.floor(Math.random() * (exhibitionList.length - 3))
        carouselList.push(...exhibitionList.slice(0, 3))

        let innerContent = ''
        for (let i = 0; i < carouselList.length; i++) {
            if (i === 0) {
                innerContent += `
                    <div class="carousel-item active">
                        <img src="${root}${carouselList[i].exhibitPImg}" class="d-block img-fluid" alt="IMAGE">
                    </div>
                `
            } else {
                innerContent += `
                    <div class="carousel-item">
                        <img src="${root}${carouselList[i].exhibitPImg}" class="d-block img-fluid" alt="IMAGE">
                    </div>
                `
            }
        }
        carousel.innerHTML = innerContent
    }
}

VModel.getData()