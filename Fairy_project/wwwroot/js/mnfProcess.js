function doLabelFirst() {
    let canvas = $("li.row").find("#canvas");
    canvas.attr("width", 800);
    canvas.attr("height", 65);
    canvas.drawPath({
        fillStyle: '#FAE9E0',
        p1: {
            type: 'line',
            x1: 0, y1: 0,
            x2: 50, y2: 33.5,
            x3: 0, y3: 65,
            x4: 200, y4: 65,
            x5: 250, y5: 32.5,
            x6: 200, y6: 0,
        }
    })
        .drawPath({
            fillStyle: '#FAE9E0',
            p1: {
                type: 'line',
                x1: 275, y1: 0,
                x2: 325, y2: 33.5,
                x3: 275, y3: 65,
                x4: 475, y4: 65,
                x5: 525, y5: 32.5,
                x6: 475, y6: 0,
            }
        })
        .drawPath({
            fillStyle: '#FAE9E0',
            p1: {
                type: 'line',
                x1: 550, y1: 0,
                x2: 600, y2: 33.5,
                x3: 550, y3: 65,
                x4: 750, y4: 65,
                x5: 800, y5: 32.5,
                x6: 750, y6: 0,
            }
        })
        .drawText({
            
            fillStyle: '#3B3131',
            strokeStyle: '#3B3131',
            strokeWidth: 1,
            fontSize: 26,
            x:125,
            y:32.5,
            fontFamily: 'Noto Sans TC',
            text:'申請資訊'

        })
        .drawText({
            
            fillStyle: '#3B3131',
            strokeStyle: '#3B3131',
            strokeWidth: 1.5,
            fontSize: 26,
            x:400,
            y:32.5,
            fontFamily: 'Noto Sans TC',
            text:'付     款'

        })
        .drawText({
            
            fillStyle: '#3B3131',
            strokeStyle: '#3B3131',
            strokeWidth: 1,
            fontSize: 26,
            x:675,
            y:32.5,
            fontFamily: 'Noto Sans TC',
            text:'申請完成'
        })
}
    $(() => {
        doLabelFirst();
    })