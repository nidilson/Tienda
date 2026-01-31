document.addEventListener('DOMContentLoaded', () => {
    const cerrarCartBtn = document.getElementById("boton-cerrar-carrito");
    const cart = document.getElementById("cart");

    cerrarCartBtn.addEventListener("click", () => {
        cart.classList.toggle("abierto");
    })
})