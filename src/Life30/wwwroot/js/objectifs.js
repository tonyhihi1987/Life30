    function grid_dataBound(e) {
        $("#Grid td").each(function () {
            var iNum = parseInt($(this).html());
            if (iNum <3)
            {
                $(this).addClass("bg-danger");
            }
            if (iNum ==3 || iNum ==4) {
                $(this).addClass("bg-info");
            }
            if (iNum ==5) {
                $(this).addClass("bg-success");
            }
                
        });
    }

    $().ready(function () {

        $.get("/Notification/GetObjectifs",
              {                  
              },
                function (obj) {                    
                    $("#appendto").html(obj);
                    var i = 1000;
                        $('.notification').each(function () {
                            $(this).fadeIn(i);
                            i += 450;
                    }
                    );
                    
                    });

        $(this).css({"background":""});
        $("#date").val("");        

        $("#content").show(500);

        $('#datetimepicker1').datetimepicker({
        });

        $("#plus").click(function () {            
            $("#plus").toggleClass("glyphicon-minus");
        })
        if ($("#valDate").html() == "The value '' is invalid.")
            $("#valDate").html("La Date est obligatoire");
        $("#submit").click(function () {
            $('#date').val($("#datePicker").val());
        });
        
        //    $('#date').removeAttr("data-val-date");
        //    $.post("/Objectif/AddTask",
        //       {
        //           tache: $("#inputTache").val(),
        //           date: $("#date").val(),
        //           commentaire: $("#commentaire").val(),
        //           nbPoint: $("#inputNbPoint").val(),
        //           type: $("#type").val()                   

        //       },
        //        function (obj) {
        //            if (obj.success) {                        
        //                        $("#realContent").html(obj.view);
        //                        $("#inputTache").val("");
        //                        $("#commentaire").val("");
        //                        $("#inputNbPoint").val("");
        //                        $('#Grid').data('kendoGrid').dataSource.read();
        //                        $('#Grid').data('kendoGrid').refresh();
        //            }
        //            else {
        //                $("#realContent").html(obj.view);
        //                $("#date").val("");

        //                $("#collapse1").toggleClass("panel-collapse collapse in");
        //            }
        //        });
        //});

        $(".dropdown-menu li a").click(function () {
            var selText = $(this).text();
            $(this).parents('.btn-group').find('.btn-default').html(selText);
            $(this).parents('.btn-group').find('.dropdown-toggle').html(' <span class="caret"></span>');
        });

        $("#nbPoint >li a").click(function () {
            var selText = $(this).text();
            $("#inputNbPoint").val(selText);
        });

        $("#tache >li a").click(function () {

            var selText = $(this).text();
            if (selText != 'Other...')
                {
                $("#inputTache").val(selText);
                $("#inputTache").hide();
                }
            else
            {
                $("#inputTache").show();
                $("#inputTache").val('');
            }
        });
    });    