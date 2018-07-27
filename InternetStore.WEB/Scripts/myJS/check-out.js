$(document).ready(function () {
    if (!$(".create-acc-checkbox").first().prop('checked')) {
        $(".create-account-block").first().attr("style", "display: none;");
        $("#GuestModel_RegModel_Password").attr("name", "");
        $("#GuestModel_RegModel_ConfirmPassword").attr("name", "");
    }
    else {
        $(".create-account-block").first().attr("style", "display: block;");
        $("#GuestModel_RegModel_Password").attr("name", "GuestModel.RegModel.Password");
        $("#GuestModel_RegModel_ConfirmPassword").attr("name", "GuestModel.RegModel.ConfirmPassword");
    }
});

$(document).on('click', '.create-acc-checkbox', function (event) {
    if (!$(".create-acc-checkbox").first().prop('checked')) {
        $("#GuestModel_RegModel_Password").attr("name", "");
        $("#GuestModel_RegModel_ConfirmPassword").attr("name", "");
    }
    else {
        $("#GuestModel_RegModel_Password").attr("name", "GuestModel.RegModel.Password");
        $("#GuestModel_RegModel_ConfirmPassword").attr("name", "GuestModel.RegModel.ConfirmPassword");
    }
});