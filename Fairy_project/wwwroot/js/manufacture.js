function doLabelFirst() {
    let canvas = document.getElementById('canvasSize')
    let context = canvas.getContext('2d')
    canvas.width = 140
    canvas.height = 31
    context.beginPath();
    context.fillStyle = "#DDCFC2";
    context.moveTo(33, 31);
    context.lineTo(38, 16);
    context.lineTo(140, 16);
    context.lineTo(135, 31);
    context.fill();
    context.beginPath();
    context.fillStyle = "#3B3131"
    context.font = "bold 32px Noto Sans TC";
    context.fillText('攤位大小', 1, 26);

}
function drawLabelSec() {
    let canvas = document.getElementById('canvasNum')
    let context = canvas.getContext('2d')
    canvas.width = 140
    canvas.height = 31
    context.beginPath();
    context.fillStyle = "#DDCFC2";
    context.moveTo(33, 31);
    context.lineTo(38, 16);
    context.lineTo(140, 16);
    context.lineTo(135, 31);
    context.fill();
    context.beginPath();
    context.fillStyle = "#3B3131"
    context.font = "bold 32px Noto Sans TC";
    context.fillText('攤位編號', 1, 26);

}
function drawLabelThird() {
    let canvas = document.getElementById('canvasDtl')
    let context = canvas.getContext('2d')
    canvas.width = 140
    canvas.height = 31
    context.beginPath();
    context.fillStyle = "#DDCFC2";
    context.moveTo(33, 31);
    context.lineTo(38, 16);
    context.lineTo(140, 16);
    context.lineTo(135, 31);
    context.fill();
    context.beginPath();
    context.fillStyle = "#3B3131"
    context.font = "bold 32px Noto Sans TC";
    context.fillText('攤位詳情', 1, 26);

}
function drawLabelForth() {
    let canvas = document.getElementById('canvasPrc')
    let context = canvas.getContext('2d')
    canvas.width = 140
    canvas.height = 31
    context.beginPath();
    context.fillStyle = "#DDCFC2";
    context.moveTo(33, 31);
    context.lineTo(38, 16);
    context.lineTo(140, 16);
    context.lineTo(135, 31);
    context.fill();
    context.beginPath();
    context.fillStyle = "#3B3131"
    context.font = "bold 32px Noto Sans TC";
    context.fillText('攤位價格', 1, 26);

}
function drawLabelFifth() {
    let canvas = document.getElementById('canvasZone')
    let context = canvas.getContext('2d')
    canvas.width = 140
    canvas.height = 31
    context.beginPath();
    context.fillStyle = "#DDCFC2";
    context.moveTo(33, 31);
    context.lineTo(38, 16);
    context.lineTo(140, 16);
    context.lineTo(135, 31);
    context.fill();
    context.beginPath();
    context.fillStyle = "#3B3131"
    context.font = "bold 32px Noto Sans TC";
    context.fillText('展覽區域', 1, 26);

}
function drawLabelSixth() {
    let canvas = document.getElementById('canvasName')
    let context = canvas.getContext('2d')
    canvas.width = 140
    canvas.height = 31
    context.beginPath();
    context.fillStyle = "#DDCFC2";
    context.moveTo(33, 31);
    context.lineTo(38, 16);
    context.lineTo(140, 16);
    context.lineTo(135, 31);
    context.fill();
    context.beginPath();
    context.fillStyle = "#3B3131"
    context.font = "bold 32px Noto Sans TC";
    context.fillText('展覽名稱', 1, 26);

}
function load() {
    doLabelFirst();
    drawLabelSec();
    drawLabelThird();
    drawLabelForth();
    drawLabelFifth();
    drawLabelSixth();
}
window.addEventListener('load', load);
let ul = $("ul#train");
//展覽區域選擇顯示
$(() => {
    let li = $("ul.tab-title li");
    $(li.eq(0).addClass("active").find("a").attr('href')).siblings(".show-list").hide();
    li.click(function () {
        $($(this).find('a').attr("href")).show().siblings(".show-list").hide();
        $(this).addClass('active').siblings(".active").removeClass("active");
    });
});
//攤位大小選擇顯示
$(() => {
    let li = $("ul.size-title li")
    $(li.eq(1).addClass("active").find("a").attr('href')).siblings(".booth-list").hide();
    li.click(function ()  {
        $($(this).find('a').attr("href")).show().siblings(".booth-list").hide();
        $(this).addClass('active').siblings(".active").removeClass("active");
    })
})
//展覽輪播
$(() => {

   
    let li = $("ul#train li")
    console.log(li.css("width"))
    const MOVE = parseInt(li.css("width").replace('px', '')) + 40;
    const MAX_ULWIDTH = parseInt(ul.css("width").replace('px', '')) + 100;
    console.log(MAX_ULWIDTH);
    let next = $(".next");
    let prev = $(".prev");
    let position = 0;

    next.click(() => {
        if (position > -MAX_ULWIDTH) {
            ul.css("transform", `translateX(${position -= MOVE}px)`)
        }

    })

    prev.click(() => {
        if (position < 0) {

            ul.css("transform", `translateX(${position += MOVE}px)`)
        }
    })

    $(() => {
        li.click((e) => {


            let itemWidth = $(e.target).eq(0).css('width').replace('px', '');
            let index = li.index
            console.log(li)
            console.log(itemWidth)
            console.log(index)
            let centerPos = (position - (index * itemWidth))
            ul.css("transform", `translateX(${centerPos}px)`)
        })
    })


    //點中到中間 該項目
})

//選擇展覽
$(() => {

    let li = $("ul.show-list li");
    $(li.eq(0).addClass("active").find("button")).siblings(".btn-woo").hide();
})


