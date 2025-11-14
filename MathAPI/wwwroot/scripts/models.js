const BASE = window.location.origin;

function showModel(name) {
    const sections = ["algebra", "ode", "vectors", "matrices"];

    sections.forEach(sec => {
        const block = document.getElementById(sec);
        block.classList.toggle("collapsed", sec !== name);
    });

    document.querySelectorAll(".tabs span").forEach(el => {
        el.classList.toggle("active", el.textContent.toLowerCase().includes(name));
    });
}

function writeOutput(text) {
    document.getElementById("output").value = text;
}

function appendHistory(text) {
    const h = document.getElementById("history");
    h.value += text + "\n";
    h.scrollTop = h.scrollHeight;
}

/* ALGEBRA API */

async function algebra(method) {
    const a = parseFloat(document.getElementById("a").value);
    const b = parseFloat(document.getElementById("b").value);

    const resp = await fetch(`${BASE}/algebra/${method}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ a, b })
    });

    const data = await resp.json();
    const text = `Algebra | ${method}(${a}, ${b}) = ${data.result}`;

    writeOutput("Result: " + data.result);
    appendHistory("Result: " + data.result);
}

/* CALCULUS API */

async function calc(method) {
    const func = document.getElementById("func").value;
    const y0 = parseFloat(document.getElementById("y0").value);
    const t0 = parseFloat(document.getElementById("t0").value);
    const dt = parseFloat(document.getElementById("dt").value);
    const n = parseInt(document.getElementById("n").value);

    const resp = await fetch(`${BASE}/calculus/${method}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ func, y0, t0, dt, n })
    });

    const data = await resp.json();
    const text = `ODE | ${method}(${func}, y0=${y0}, t0=${t0}, dt=${dt}, n=${n}) = ${data.result}`;

    writeOutput("Result: " + data.result);
    appendHistory("Result: " + data.result);
}

function parseVector(str) {
    return str.split(",").map(Number);
}

/* VECTOR API */

async function vector(method) {
    const A = parseVector(document.getElementById("vA").value);
    const B = parseVector(document.getElementById("vB").value);

    const resp = await fetch(`${BASE}/vectors/${method}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ A, B })
    });

    const data = await resp.json();
    const text = `Vector | ${method}([${A}], [${B}]) = ${data.result}`;

    writeOutput("Result: " + data.result);
    appendHistory(text);
}

function parseMatrix(str) {
    return str.split(";").map(
        row => row.split(",").map(Number)
    );
}

/* MATRIX API */

async function matrix(method) {
    const A = parseMatrix(document.getElementById("mA").value);
    let payload = { A };

    // If method requires two matrices, include B
    if (method === "add" || method === "multiply") {
        const B = parseMatrix(document.getElementById("mB").value);
        payload = { A, B };
    }

    const resp = await fetch(`${BASE}/matrices/${method}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });

    const data = await resp.json();
    const text = `Matrix | ${method}(${JSON.stringify(payload)}) = ${JSON.stringify(data.result)}`;

    writeOutput("Result: " + JSON.stringify(data.result));
    appendHistory(text);
}

document.addEventListener("DOMContentLoaded", () => {
    const titles = document.querySelectorAll(".model-title");

    titles.forEach(t => {
        t.addEventListener("click", () => {
            const block = t.closest(".model-block");
            block.classList.toggle("collapsed");
        });
    });
});

