function eliminar(iduu) {

    var formData = new FormData();
    formData.append("idusuario", iduu);
    //alert("recibido"+iduu);
    Swal.fire({
        title: 'Estas seguro eliminar este registro?',
        text: "No puedes revertir los cambios!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, Eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Registrarse/Registro/Eliminar',
                type: 'post',
                data: { id: iduu },
                //contentType: false,
                //processData: false,
                success: function (response) {

                    Swal.fire(
                        'Eliminar!',
                        'Tu registro se eliminó correctamente.',
                        'success',
                        document.location.href = "/Registrarse/Registro/ListarUsuarios.cshtml"
                    )
                }
            });
        }
    })
}
function Modificar(modificar) {
    Swal.fire(
        'Good job!',
        'You clicked the button!',
        'success'
    )

}
