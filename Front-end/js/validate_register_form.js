var registerForm = $("#register-form");
registerForm.validate({

    // Specify the validation rules
    rules: {
        employee_code: {
            required: true
        },
        fullname: {
            required: true
        }
    },
    messages: {
        employee_code: "Không bỏ trống mã NV",
    }

});

registerForm.on("submit", function(e){
    e.preventDefault();
});

$('#button-add').on("click", function () {
    var registerData ={};
    if (registerForm.valid()) {
        $("#register-form input").each(function(){
            var input = $(this);
            if(input.attr("type") !== "radio"){
                registerData[input.attr("name")] = input.val()?input.val():"";
            } else{
                if(input.is(":checked")){
                    registerData[input.attr("name")] = input.val()?input.val():"";
                }
            }
        })
    }
    console.log(JSON.stringify(registerData));
});