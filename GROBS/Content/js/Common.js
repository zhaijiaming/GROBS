//配置表格
function table_config(id) {
    var mydatatable = id;
    var th_new = $("#mydatatable tr").eq(0).clone();
    $(".fixed-table-header").append(th_new);
    trResize();//初始化表头
    $("#sidebar").click(function () {//导航栏大小变动初始化表头
        trResize();
    });
    $(window).resize(function () {//浏览器大小变动初始化表头
        trResize();
    });
    $(".table-wapper").scroll(function () {//固定表头跟随X轴滚动，逆向滚动
        var scroll = -$(this).scrollLeft();
        $(".fixed-table-wapper").css("left", scroll + "px");
    });
    $("#mydatatable tr").click(function () {//单击tr选中
        //console.log($(this).attr('class'));
        if ($(this).attr('class') != "success") {
            $(this).addClass("success").siblings().removeClass("success").end();
        } else {
            $(this).removeClass("success").end();
        }
    });
    //统一设置CSS样式
    $(".fixed-table-header tr").css("background-color", "#e1edfe"); //固定表头颜色
    $("#mydatatable tr:odd").css("background-color", "#f2f7fe"); //偶行颜色
    $("#mydatatable tr:even").css("background-color", "#e1edfe"); //奇行颜色
    $(".table-wapper-all").css({
        "position": "relative",
        "overflow": "hidden",
    });
    $(".fixed-table-wapper").css({
        "position": "absolute",
        "top": "0px",
        "left": "0px",
        "background-color": "#fff",
        "border-bottom": "1px solid #ddd",
    });
    $(".fixed-table-header").css("margin-bottom", "0px");
    $(".table-wapper").css({
        "height": "420px",
        "overflow-y": "auto",
    });
}
//设置固定表头的表格大小
function trResize() {
    var inner_width = $("#mydatatable").outerWidth();
    $(".fixed-table-wapper").css("width", inner_width + "px");
    $(".fixed-table-header th").each(function () {
        var num = $(this).index();
        var th_width = $("#mydatatable th").eq(num).outerWidth();
        var th_height = $("#mydatatable th").eq(num).outerHeight();
        //console.log("th_width:" + num + ":" + th_width);
        $(this).css("width", th_width + "px");
        $(this).css("height", th_height + "px");
    });
}
function checkedByClick(name) {
    var tableName = name;
    tableName.on("click", "tr", function (e) {
        if (e.target.tagName == "TD" || e.target.tagName == "SPAN") {
            if ($(this).find("input[type=checkbox]").is(':checked')) {
                $(this).find(":checkbox").prop("checked", false);
            } else {
                tableName.find("input[type=checkbox]").prop("checked", false);
                $(this).find(":checkbox").prop("checked", true);
            }
        }
    });
}