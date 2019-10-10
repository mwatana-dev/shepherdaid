
$(document).ready(function () {


    //alert("member select list working.");

    /*------------------------------Available Roles--------------------------------------*/
    //Ensure that at least one available role is available
    $(document).on("click", "#btn-assign", function () {
        var count = $('#div-available-item input[type="checkbox"]:checked').length;
        if (count < 1) {
            alert("No item is available. Select at least one item.");
            return false;
        }
    });

    //on click assign$('#check-all-available').click

    //check and uncheck all roles when the Check All is clicked
    $("#check-all-available").click(function () {
        var i = 0;
        if ($("#check-all-available").is(':checked')) {
            $('#div-available-item input[type=checkbox]').each(function () {
                $(this).prop('checked', true);
            });
        }
        else {
            $('#div-available-item input[type=checkbox]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });//click on check-all-available

    //check and uncheck the check-all-available based on roles checked
    $(document).on("click", "#available", function () {

        var allcount = $('#div-available-item input[type="checkbox"]').length;
        var checkedcount = $('#div-available-item input[type="checkbox"]:checked').length;

        if (checkedcount == allcount) {
            $("#check-all-available").prop("checked", true);
        }
        else {
            $("#check-all-available").prop("checked", false);
        }
    });//click on available roles


    /*-------------assigned roles---------------------------*/
    //Ensure that at least one available role is assigned
    $(document).on("click", "#btn-revoke", function () {
        var count = $('#div-assigned-item input[type="checkbox"]:checked').length;
        if (count < 1) {
            alert("No item is assigned. Select at least one item.");
            return false;
        }
    });

    //on click assign$('#check-all-assigned').click

    //check and uncheck all roles when the Check All is clicked
    $("#check-all-assigned").click(function () {
        var i = 0;
        if ($("#check-all-assigned").is(':checked')) {
            $('#div-assigned-item input[type=checkbox]').each(function () {
                $(this).prop('checked', true);
            });
        }
        else {
            $('#div-assigned-item input[type=checkbox]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });//click on check-all-assigned

    //check and uncheck the check-all-assigned based on roles checked
    $(document).on("click", "#assigned", function () {

        var allcount = $('#div-assigned-item input[type="checkbox"]').length;
        var checkedcount = $('#div-assigned-item input[type="checkbox"]:checked').length;

        if (checkedcount == allcount) {
            $("#check-all-assigned").prop("checked", true);
        }
        else {
            $("#check-all-assigned").prop("checked", false);
        }
    });//click on available roles

});//document ready
