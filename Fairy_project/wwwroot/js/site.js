//控制Scroll位置 Navbar


const nav = document.querySelector(".navbar");

window.addEventListener("scroll", () => {
    const scrollHeight = window.scrollY;
    if (scrollHeight > 52) {
        nav.classList.add("nav_onscroll"); 
    } else if (scrollHeight <= 52) {
        nav.classList.remove("nav_onscroll");
    }
});

