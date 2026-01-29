document.addEventListener('DOMContentLoaded', () => {
    var menuBtn = document.getElementById("menu-btn");
    var navLinks = document.getElementById("nav-links");
    menuBtn.addEventListener("click", () => {
        menuBtn.classList.toggle("active");
        navLinks.classList.toggle("active");
    })
})
