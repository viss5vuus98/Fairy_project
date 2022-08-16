function doLabelFirst() {
    let canvas = document.getElementById('canvasRole')
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
    context.fillText('法律規定', 1, 26);

}
function drawLabelSec() {
    let canvas = document.getElementById('canvasRule')
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
    context.fillText('會場規定', 1, 26);

}
$(() => {
    doLabelFirst();
    drawLabelSec();
})