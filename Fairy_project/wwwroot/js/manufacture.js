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
function load() {
    doLabelFirst();
    drawLabelSec();
    drawLabelThird();
    drawLabelForth();
}

window.addEventListener('load', load);