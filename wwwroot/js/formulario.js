

$("#signupform").validate({
    rules: {
        cedula: {
            required: true,
            minlengh: 8,
            maxlengh: 10
        },
        nombre: {
            required: true,
            minlengh: 8,
            maxlengh: 10
        },
        email: {
            required: true,
            email: true,
        },
        usuario: {
            required: true,
            minlengh: 4,
            maxlengh: 10
        },
        pass: {
            required: true,
            minlengh: 4,
            maxlengh: 15
        }
    }
})



$("#registrar").click(function () {
    if ($("#signupform").valid() == false) {
        return;
    }
    let cedula = $("#cedula").val();
    let nombre = $("#nombre").val();
    let email = $("#email").val();
    let usuario = $("$usuario").val();
    let pass = $("#pass").val();


})