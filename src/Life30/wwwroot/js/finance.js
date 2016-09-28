
    $().ready(function () {
        $("#submit").click(function () {
            $('#date').val($("#datePicker").val());
            $('#type').val($("#btnType").html());
        });

        $("#types >li a").click(function () {

            $("#btnType").html($(this).text());
        });
    });    