//取得今天日期
var today = new Date();
var currentDateTime =
    today.getFullYear() + '年' + (today.getMonth() + 1) + '月' + today.getDate() + '日';

$(".second").hide();
$(".third").hide();
//繼續選購
$("keepbuy").click(function () {

})
//動態生成
$(".first").on("click", "#add", function () {
    $(".ticket").append(`<div class="container" style="border: 1px solid black;">
                <div class="rowticket d-flex">
                    <div class="col-xl-3"><img
                            src="https://images.unsplash.com/photo-1535385793343-27dff1413c5a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8bXVzZXVtfGVufDB8MXwwfHw%3D&auto=format&fit=crop&w=500&q=60"
                            alt=""></div>
                    <div class="col-xl-9">
                        <div class="d-flex" style="justify-content: space-between;">
                            <h3>2022 NTT-TIFA M&B雙人組(大湖) 劇場特映暨展覽</h3>
                            <button type="button" class="btn btn-secondary"><i
                                    class="fa-solid fa-trash-can"></i></button>
                        </div>
                        <h5>2022/4/30(六) 14:30</h5>
                        <h5>台中國家歌劇園中劇院</h5>
                        <div class="price">
                            <div class="d-flex originprice">
                                原價$
                                <p>200</p>元
                            </div>
                            <div class="d-flex" style="align-items:center">
                                數量:<button type="button" class="minus"><i class="fa-solid fa-minus"></i></button>
                                <input type="text" style="width:50px;text-align: center;" class="count" value="1"
                                oninput="value=value.replace(/[^0-9]/g,'')">
                                <button type="button" class="plus"><i class="fa-solid fa-plus"></i></button>
                            </div>
                            <div class="d-flex smallcount">

                            </div>
                        </div>
                    </div>
                </div>
            </div>`)
    $(".minus").attr('disabled', true);
    $(this).next().find(".smallcount").last().append(`小計:<p>${parseInt($(this).next().find(".originprice").last().children().text()) * $(this).next().find(".count").last().val()}</p>`);
    // 這裡不是增加按鈕時要改$選擇器內容

    sum = 0;
    var smallcount = $(this).next().find(".smallcount p");
    for (let i = 0; i < smallcount.length; i++) {
        sum += parseInt(smallcount.eq(i).text());
    }
    console.log(sum);
    $("#totalprice").text("");
    $("#totalprice").append(`總金額:<p>${sum}</p>元`);
    total = sum;
})
//按鈕事件
var sum = 0;
$(".ticket").on("click", ".btn-secondary", function () {
    $(this).parent().parent().parent().parent().text("").css("border", "0");
    console.log($(this).closest(".col-xl-9").find(".smallcount p"));
    sum = 0;
    var smallcount = $(":not(this)").closest(".col-xl-9").find(".smallcount p");
    for (let i = 0; i < smallcount.length; i++) {
        sum += parseInt(smallcount.eq(i).text());
    }
    console.log(sum);
    $("#totalprice").text("");
    $("#totalprice").append(`總金額:<p>${sum}</p>元`);
    total = sum;
})

$(".ticket").on("keyup", ".count", function () {
    if (parseInt($(this).val()) < 1) {
        alert("請輸入大於0的正整數")
    }
    if (parseInt($(this).val()) > 1) {
        $(this).prev().attr('disabled', false);
    } else {
        $(this).prev().attr('disabled', true);
    }
    $(this).parent().next().text("")
    $(this).parent().next().append(`小計:<p>${parseInt($(this).parent().prev().children().text()) * $(this).val()}</p>`);
    //購物車總金額
    sum = 0;
    var smallcount = $(this).closest(".ticket").find(".smallcount p");
    for (let i = 0; i < smallcount.length; i++) {
        sum += parseInt(smallcount.eq(i).text());
    }
    console.log(sum);
    $("#totalprice").text("");
    $("#totalprice").append(`總金額:<p>${sum}</p>元`);
    total = sum;
})

$(".ticket").on("click", ".plus", function () {
    $(this).prev().val(parseInt($(this).prev().val()) + 1);
    if (parseInt($(this).prev().val()) > 1) {
        $(this).prev().prev().attr('disabled', false);
    } else {
        $(this).prev().prev().attr('disabled', true);
    }
    $(this).parent().next().text("")
    $(this).parent().next().append(`小計:<p>${parseInt($(this).parent().prev().children().text()) * $(this).prev().val()}</p>`);
    //購物車總金額
    sum = 0;
    var smallcount = $(this).closest(".ticket").find(".smallcount p");
    for (let i = 0; i < smallcount.length; i++) {
        sum += parseInt(smallcount.eq(i).text());
    }
    console.log(sum);
    $("#totalprice").text("");
    $("#totalprice").append(`總金額:<p>${sum}</p>元`);
    total = sum;
})

$(".ticket").on("click", ".minus", function () {
    $(this).next().val(parseInt($(this).next().val()) - 1);
    if (parseInt($(this).next().val()) <= 1) {
        $(this).attr('disabled', true);
    }
    $(this).parent().next().text("")
    $(this).parent().next().append(`小計:<p>${parseInt($(this).parent().prev().children().text()) * $(this).next().val()}</p>`);
    //購物車總金額
    sum = 0;
    var smallcount = $(this).closest(".ticket").find(".smallcount p");
    for (let i = 0; i < smallcount.length; i++) {
        sum += parseInt(smallcount.eq(i).text());
    }
    console.log(sum);
    $("#totalprice").text("");
    $("#totalprice").append(`總金額:<p>${sum}</p>元`);
    total = sum;
})

//前往付款
$("#gopay").click(function () {
    if ($("*.count").val() == "") {
        alert("數量不能為空值")
    } else {
        console.log(sum);
        $(window).scrollTop(0);
        $(".first").hide();
        $(".second").show();
        $(".third").hide();
        $("#totalprice1").text("");
        $("#totalprice1").append(`總金額:<p>${sum}</p>元`);
    }
})

//上一步
$("#pre").click(function () {
    $(window).scrollTop(0);
    $(".first").show();
    $(".second").hide();
    $(".third").hide();
})

//submit表單送出
$("#pay").click(function () {
    $("#name").val();
    $("#cellphone").val();
    $("#Email").val();
    $(".radio input:radio[name=option]:checked").val();
    $(".orderprice").append(`<h5>${total}</h5>元`);
    $(".ordertime").append(`<h5>${currentDateTime}</h5>`)
    $(".ordername").append(`<h5>${$("#name").val()}</h5>`);
    $(".orderphone").append(`<h5>${$("#cellphone").val()}</h5>`);
    $(".orderemail").append(`<h5>${$("#Email").val()}</h5>`);
    alert("您已成功付款");
    $(window).scrollTop(0);
    $(".first").hide();
    $(".second").hide();
    $(".third").show();
})
$("#keepshop").click(function () {

})

        //判斷表單輸入事件