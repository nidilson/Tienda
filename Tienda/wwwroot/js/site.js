document.addEventListener('DOMContentLoaded', () => {
    const menuBtn = document.getElementById("menu-btn");
    const navLinks = document.getElementById("nav-links");

    const cartBtn = document.getElementById("cart-toggle");
    const cart = document.getElementById("cart");

    const agregarBtn = document.querySelectorAll(".agregar-btn");

    menuBtn.addEventListener("click", () => {
        menuBtn.classList.toggle("active");
        navLinks.classList.toggle("active");
    })

    cartBtn.addEventListener("click", () => {
        cart.classList.toggle("abierto");
    })

    agregarBtn.forEach(btn => {
        btn.addEventListener("click", async (evt) => {
            await AgregarACarrito(evt.target.dataset.id);
        })
    })
})

async function AgregarACarrito(idProd) {
    try {
        const response = await fetch("/DetalleCarrito", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "producto_id": idProd,
                "cantidad": 1
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
