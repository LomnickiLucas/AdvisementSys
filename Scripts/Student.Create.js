$(function () {
    $("form").submit(function (e) {
        $.post($(this).attr("action"),
    $(this).serialize(),
    function (data) {
        openBlock(data);
    });
        e.preventDefault();
    });
});