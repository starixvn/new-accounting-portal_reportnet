var form = $('#divPassword');
var oldPassword = form.find('#txtOldPassword');
var newPassword = form.find('#txNewPassword');
var confirmPassword = form.find('#txtConfirmPassword');
var msgArea = $('#divErrorSuccessMsg', form);

var validateMatchedPass = function () {
    var err = "'New Password' and 'Confirm Password' do not match";

    if (newPassword.val() !== confirmPassword.val()) {
        Validator.showError(confirmPassword, err, 'w');
        return false;
    } else {
        return true;
    }
};

var showInlineErrorMsg= function (message, $obj) {
    $obj.html(message);
    $obj.removeClass('success').addClass('error').slideDown();
};
var showInlineSuccessMsg = function (message, $obj) {
    $obj.html(message);
    $obj.removeClass('error').removeClass('info').addClass('success').slideDown();

    setTimeout(function () {
        $obj.removeClass('error').removeClass('info');
        $obj.html('');
    }, 3000);
};

$(function () {
    if (isSuccess) {
        showInlineSuccessMsg('Your changes were saved!', msgArea);
    } else if (message !== '') {
        showInlineErrorMsg(message, msgArea);
    }

    $('#btnSubmit').click(function () {
        if (!Validator.validateForm(form, 'w')) {
            return false;
        } else if (!validateMatchedPass()) {
            return false;
        }
    });
});