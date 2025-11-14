document.addEventListener("DOMContentLoaded", () => {
    function createParticle() {
        const p = document.createElement("div");
        p.className = "particle";

        // random pixel scale
        const size = Math.random() * 2 + 2;
        p.style.width = `${size}px`;
        p.style.height = `${size}px`;

        // random position
        p.style.left = `${Math.random() * 100}%`;
        p.style.top  = `${Math.random() * 100}%`;

        // smoother animation
        const duration = Math.random() * 8 + 15;   // 12–20 sec
        const delay    = Math.random() * 3;        // 0–3 sec
        p.style.animationDuration = `${duration}s`;
        p.style.animationDelay    = `${delay}s`;

        document.body.appendChild(p);

        // cleanup AFTER fade — prevent buildup
        setTimeout(() => p.remove(), (duration + delay) * 750);
    }

    setInterval(createParticle, 700);
});