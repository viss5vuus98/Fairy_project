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
function load() {
    doLabelFirst();
    drawLabelSec();
    drawLabelThird();
    drawLabelForth();
    drawLabelFifth();
}
window.addEventListener('load', load);

//展覽區域選擇顯示
$(() => {
    let li = $("ul.tab-title li");
    $(li.eq(0).addClass("active").find("a").attr('href')).siblings(".show-list").hide();
    li.click(function () {
        $($(this).find('a').attr("href")).show().siblings(".show-list").hide();
        $(this).addClass('active').siblings(".active").removeClass("active");
    });
});
//展覽輪播
$(() => {
    
    let ul = $("ul#train");
    let li = $("ul#train li")
    console.log(li.css("width"))
    const MOVE = parseInt(li.css("width").replace('px', '')) + 40;
    const MAX_ULWIDTH = parseInt(ul.css("width").replace('px', ''))+200;
    let next = $("#btn-next");
    let prev = $("#btn-prev");
    let position = 0;
    
    next.click((e) => {
        if (position > -MAX_ULWIDTH) {
            ul.css("transform", `translateX(${position -= MOVE}px)`)
        }

    })

    prev.click((e) => {
        if (position < 0) {

            ul.css("transform", `translateX(${position += MOVE}px)`)
        }
    })
    li.click((e) => {
       
        console.log(li[e.target].
    })

    //點中到中間 該項目
})
//選擇展覽
$(() => {

    let li = $("ul.show-list li");
    $(li.eq(0).addClass("active").find("button")).siblings(".btn-woo").hide();
})

