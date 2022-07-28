//Render Exhibitions

const VModel = {
    getData() {
        axios.get('/Home/getAllExhibition')
            .then(res => {
                const exhibitionList = []
                exhibitionList.push(...res.data)
                View.renderExhibitions(exhibitionList)
            })
            .catch(err => console.log(err))
    },

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
                        <img src="" class="img-fluid" alt="this is a title">
                        <div class="overlay">
                            <div class="buttons">
                                <a rel="gallery" class="fancybox" href="images/portfolio/item-1.jpg">Demo</a>
                                <a target="_blank" href="single-portfolio.html">Details</a>
                            </div>
                        </div>
                    </div>
                    <figcaption>
                        <h4>
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
    },
}

VModel.getData()