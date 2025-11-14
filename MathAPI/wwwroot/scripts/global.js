function setupHeader() {
    const burger = document.getElementById("qlBurger");
    const links = document.getElementById("qlLinks");

    console.log("setupHeader() called", burger, links);   // debugging

    if (!burger || !links) {
        console.warn("Header items missing!");
        return;
    }

    burger.addEventListener("click", () => {
        links.classList.toggle("show");
        console.log("CLICK");
    });
}