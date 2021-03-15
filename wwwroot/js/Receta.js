let ingredientes = document.getElementById("ingredientes");
let cont = 0;
ingredientes.addEventListener("click", (e) => {
    recolectar(e);
   
});

let recolectar = e => {
    if (e.target.classList.contains('btn', 'btn-danger')) {
        organizar(e.target.parentElement);
    }
}

let sacarNombre = (e, u) => {
    let Producto = [];
    let tipoProd = e.substring(12);
    let descripProd = u.substring(12);
    Producto.push(tipoProd, descripProd);
    return Producto;
}

let organizar = e => {
    console.log(e);
    let tipo = e.querySelector("label").textContent;
    let divDescripcion = e.querySelector('.card-footer', 'text-center');
    let descripcion = divDescripcion.querySelector("label").textContent;
    let elementoiD = e.querySelector('input');
    let id = elementoiD.value;
    let divCantidad = e.querySelector('input[name="Cantidad"]');
    let cantidad = divCantidad.value;
    let img = e.querySelector('img');
    let imagenProducto = img.src;
    let ArrayLetras = sacarNombre(tipo, descripcion);
    [tipoP, DescripP] = ArrayLetras;
    let nombre = document.getElementById("nombre").value;
    let receta = {
        cont,
        nombre,
        id,
        tipoP,
        cantidad,
        imagenProducto,
        DescripP,
    }
    if (localStorage.getItem("recetas") == null) {
        let Recetas = [];
        Recetas.push(receta);
        localStorage.setItem("recetas", JSON.stringify(Recetas));
    } else {
        let objetos = JSON.parse(localStorage.getItem("recetas"));
        objetos.push(receta);
        localStorage.setItem("recetas", JSON.stringify(objetos));
    }
    mostrarRecetas();
    cont++;
}


var mostrarRecetas = () => {
    let objetos = JSON.parse(localStorage.getItem("recetas"));
    if (objetos != null) {
        let tabla = document.getElementById("tbody");
        tabla.innerHTML = '';
        objetos.forEach(element => {
            tabla.innerHTML += `
         <tr>
           <td>
           <input type="hidden" name="Receta[${cont}].idingrediente" value="${element.id}"/>
            ${element.id}
        </td>
         <td>${element.nombre}</td>
         <td>${element.DescripP}</td>
         <td><img src="${element.imagenProducto}" width="100px" heigth="90px"/></td>
         <td>${element.tipoP}</td>
         <td>${element.cantidad}</td>
         <td><button type="button" class="btn btn-danger" onclick="eliminar(${element.id})">Eliminar</button>
         <input id="Cantidad" name="Cantidad" type="hidden" value="${element.cantidad}"/>
         <input id="NombreReceta" name="RecetaNombre" type="hidden" value="${element.nombre}"/>
         <input id="IngredienteId" name="IngredienteId" type="hidden" value="${element.id}"/>
         
         </td>
         </tr>
       `;
        })
       
        console.log(cont);
    }

}

mostrarRecetas();

let eliminar = (e) => {
    let objetos = JSON.parse(localStorage.getItem("recetas"));
    let posicion = objetos.findIndex(elemento => elemento.id == e);
    console.log(posicion);
    objetos.splice(posicion, 1);
    localStorage.setItem("recetas", JSON.stringify(objetos));
    mostrarRecetas();
}

//envioDatosRecetas();