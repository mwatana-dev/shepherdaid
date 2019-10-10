
$(document).ready(function () {


    //alert("member select list working.");

    /*------------------------------Available Roles--------------------------------------*/

    //Ensure that at least one available role is selected
    $(document).on("click", "#btn-submit", function () {
        var count = $('#div-available-item input[type="checkbox"]:checked').length;
        if (count < 1) {
            alert("No item is selected. Select at least one item.");
            return false;
        }
    });//on click assign$('#check-all').click

    //check and uncheck all roles when the Check All is clicked
    $("#check-all").click(function () {
        var i = 0;
        if ($("#check-all").is(':checked')) {
            $('#div-available-item input[type=checkbox]').each(function () {
                $(this).prop('checked', true);
            });
        }
        else {
            $('#div-available-item input[type=checkbox]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });//click on check-all

    //check and uncheck the check-all based on roles checked
    $(document).on("click", "#selected", function () {

        var allcount = $('#div-available-item input[type="checkbox"]').length;
        var checkedcount = $('#div-available-item input[type="checkbox"]:checked').length;

        if (checkedcount == allcount) {
            $("#check-all").prop("checked", true);
        }
        else {
            $("#check-all").prop("checked", false);
        }
    });//click on available roles

});//document ready
