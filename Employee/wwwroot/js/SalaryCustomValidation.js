$.validator.addMethod("salarycustomvalidation", function (value, element, params) {
    var Salary = String(value);
    var Province = $(params).val();  //var Province = params[0]; 
    console.log(Province);
    if (Province == 2) { // select option value 2 for Quebec
        var pattern = /^[0-9]{2}[\s\,]?[0-9]{3}((\.[0-9]{2})|(\,[0-9]{2}))?$/;
        return pattern.test(Salary);
    }
    else {
        var pattern = /^[0-9]{2}[\,]?[0-9]{3}(\.[0-9]{2})?$/
        return pattern.test(Salary);
    }
});

$.validator.unobtrusive.adapters.add("salarycustomvalidation", ["otherpropertyname"], function (options) {
    options.rules["salarycustomvalidation"] = options.params.otherpropertyname;
    options.messages["salarycustomvalidation"] = options.message;
});