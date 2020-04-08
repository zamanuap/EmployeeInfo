$.validator.addMethod("namecustomvalidation", function (value, element, params) {
    var Name = String(value);
    
    console.log(Name);
    
    var pattern = /^[\P{M}\p{M}]{2,}$/u;
        return pattern.test(Name);
    
});

$.validator.unobtrusive.adapters.add("namecustomvalidation", ["propertyname"], function (options) {
    options.rules["namecustomvalidation"] = options.params.propertyname;
    options.messages["namecustomvalidation"] = options.message;
});