document.addEventListener("click", (e) => {
    if (e.target.matches(".boton-cerrar-carrito")) {
        const cart = document.getElementById("cart");
        cart.classList.toggle("abierto");
    }
});