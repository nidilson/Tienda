document.addEventListener("click", (e) => {
    if (e.target.matches(".boton-cerrar-carrito")) {
        const cart = document.getElementById("cart");
        cart.classList.toggle("abierto");
    }
    if (e.target.matches(".cant-btn")) {
        const idProducto = e.target.dataset.idProducto;
        let cantidadActual = e.target.dataset.cantidad;
        if (e.target.matches(".aumentar-cantidad")){
            cantidadActual++;
        }
        if (e.target.matches(".disminuir-cantidad")){
            cantidadActual--;
        }
        ActualizarCantidadCarrito(idProducto, cantidadActual)
    }
    if (e.target.matches(".eliminar-btn")) {
        const idDetalle = e.target.dataset.idDetalle;
        EliminarDetalleCarrito(idDetalle)
    }
});

async function EliminarDetalleCarrito(idDetalle) {
    try {
        const response = await fetch("/DetalleCarrito", {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "detalle_id": idDetalle
            })
        });
        if (!response.ok) {
            throw new Error(`Error del servidor: ${response.status}: ${response.statusText} - ${data.message}`)
        }
        ObtenerCarrito();
    } catch (e) {
        console.error("Error al agregar el producto: " + e.message)
    }
}

async function ActualizarCantidadCarrito(idProd, cantidad) {
    try {
        const response = await fetch("/DetalleCarrito", {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "producto_id": idProd,
                "cantidad": cantidad
            })
        });
        if (!response.ok) {
            throw new Error(`Error del servidor: ${response.status}: ${response.statusText} - ${data.message}`)
        }
        ObtenerCarrito();
    } catch (e) {
        console.error("Error al agregar el producto: " + e.message)
    }
}

async function ObtenerCarrito() {
    const carritoContainer = document.getElementById("cart");
    try {
        const response = await fetch("/DetalleCarrito", {
            method: "GET"
        });
        if (!response.ok) {
            throw new Error("Error del servidor: " + response.status + response.statusText)
        }
        const data = await response.text();
        carritoContainer.innerHTML = data;
    } catch (e) {
        console.error("Error al crear el carrito: " + e.Message)
    }

}