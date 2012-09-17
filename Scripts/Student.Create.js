$(function () {

    $("form").submit(function (e) {
        if ($("#studentForm").valid()) {
            $.post($(this).attr("action"),
    $(this).serialize(),
    function (data) {
        openBlock(data);
    });
           // e.preventDefault();
        }
    });

});