
axios.interceptors.request.use(function (config) {
    // 在發送请求之前

    NProgress.start();
    return config;
}, function (error) {
    // 請求錯誤

    return Promise.reject(error);
});

// 響應攔截器//

axios.interceptors.response.use((response) => {
    NProgress.done();
    return response
}, (error) => {
    return Promise.reject(error)
})

const view = {
    dealtickets(ticketContents) {
        for (let i = 0; i < ticketContents.length; i++) {
            ticketContents[i].classList.remove("dealCards")
        }
        for (let i = 0; i < ticketContents.length; i++) {
            setTimeout(() => {
                ticketContents[i].classList.add("dealCards")
            }, 75 * (i + 10))
        }
    }
}


new Vue({
    el:'#app',
    data: {
        exhibitList: [],
        ticketsList: [],
        memberId: JSON.parse(sessionStorage.getItem("Info")).id,
        root: root,
        QRTtitle: '',
        orderNum: 0,
        exid: 0,
    },
    methods: {
        getData() {
            axios.post('/api/Member/Post/getTicketsss', { "Mf_id": this.memberId })
                .then(res => {
                    this.exhibitList.push(...res.data.exhibition)
                })
                .then(() => {
                    this.getTicket(this.exhibitList[0].exhibitId)
                })
                .catch(err =>
                    console.log(err)
                )

        },
        getTicket(exid) {
            //const exhibition = exhibitList.find(exhibit => exhibit.exhibitId === exid)

            axios.post('/api/Member/Post/getTicketOfExhibit', { "Ex_id": exid, "Mf_id": this.memberId })
                .then(res => {
                    this.ticketsList.push(...res.data)
                })
                .then(() => {
                    view.dealtickets(document.querySelectorAll('.ticket-content'))
                })
                .catch(err => {
                    console.log(err)
                })
        },
        getImage(image) {
            return `${this.root}${image}`
        },
        dateStart(dateFrom) {
            return new Date(dateFrom)
        },
        dateEnd(dateTo) {
            return new Date(dateTo)
        },
        selectExhibit(exhibit) {
            axios.post('/api/Member/Post/getTicketOfExhibit', { "Ex_id": exhibit.exhibitId, "Mf_id": this.memberId })
                .then(res => {
                    this.ticketsList.length = 0
                    this.ticketsList.push(...res.data)                  
                })
                .then(() => {
                    view.dealtickets(document.querySelectorAll('.ticket-content'))
                })
                .catch(err => {
                    console.log(err)
                })
        },
        showQRcodeModal(item) {
            const QRBody = document.querySelector('#QR-body');
            axios.post('/api/Member/Post/getVerificationCode', { "Ex_id": item.exhibit.exhibitId, "Mf_id": item.ticket.orderNum })
                .then(res => {
                    this.exid = item.exhibit.exhibitId
                    this.QRTtitle = res.data.exhibitName;
                    QRBody.innerHTML = '<div id="QRcode"></div>'
                    new QRCode(document.getElementById("QRcode"), {
                        text: res.data.verificationCode,
                        width: 300,
                        height: 300,
                        colorDark: "#000000",
                        colorLight: "#ffffff",
                        correctLevel: QRCode.CorrectLevel.H
                    });

                })
                .catch(err => console.log(err))
        },
        giveTicketModal(item) {
            this.orderNum = item.ticket.orderNum
            this.exid = item.exhibit.exhibitId
        },
        giveSubmit(exid) {
            const email = document.querySelector('#give_modal_email').value
            if (email.trim().length > 0) {
                console.log(this.orderNum)
                axios.post('/api/Member/Post/giveTicket', { "eamil": email, "order": this.orderNum })
                    .then((res) => {
                        console.log(res.data)
                        this.ticketsList = this.ticketsList.filter(_ticket => _ticket.ticket.orderNum !== this.orderNum)
                        swal({
                            title: "成功寄送!",
                            text: "成功",
                            icon: "success",
                            button: "返回",
                        });
                    })
                    .then(() => {
                        bootstrap.Modal.getInstance(document.querySelector('#give_Modal')).hide()                        
                    })
                    .catch(err => console.log(err))
            } else {
                swal("輸入錯誤!", "請重新輸入信箱!");
            }
        },
        reminTicket() {
            axios.post('/api/Member/Post/getTicketOfExhibit', { "Ex_id": this.exid, "Mf_id": this.memberId })
                .then(res => {
                    this.ticketsList.length = 0
                    this.ticketsList.push(...res.data)
                })
                .then(() => {
                    view.dealtickets(document.querySelectorAll('.ticket-content'))
                })
                .catch(err => {
                    console.log(err)
                })
        }
    },
    mounted() {
        this.getData()       
    },
    computed: {
        remaining() {
            return this.ticketsList
        },
    }
})

