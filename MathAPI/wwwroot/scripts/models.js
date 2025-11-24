const BASE = window.location.origin;

function showModel(name) {
    const sections = ["algebraBlock", "odeBlock", "vectorsBlock", "matricesBlock"];

    sections.forEach(sec => {
        const block = document.getElementById(sec);
        block.classList.toggle("collapsed", sec !== name);
    });

    document.querySelectorAll(".tabs span").
        forEach(el => {el.classList.toggle("active", el.textContent.toLowerCase().includes(name));
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
    appendHistory("Algebra | " + method + " => " + data.result);
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
    appendHistory("ODE | " + method + " " + func + " => " + JSON.stringify(data.result));
}

/* VECTOR API */

function parseVector(str) {
    return str.split(",").map(n => Number(n.trim()));
}

async function vector(method) {
    const A = parseVector(document.getElementById("vA").value);
    const B = parseVector(document.getElementById("vB").value);

    const resp = await fetch(`${BASE}/linearalgebra/vector/${method}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            vectorA: A,
            vectorB: B
        })
    });

    const data = await resp.json();

    // dot endpoint returns DotProduct instead of Result
    const result = data.Result ?? data.DotProduct;

    writeOutput("Result: " + JSON.stringify(result));
    appendHistory("Vector | " + method + " => " + JSON.stringify(result));
}

/* MATRIX API */

function parseMatrix(str) {
    return str.split(";").map(row =>
        row.split(",").map(n => Number(n.trim()))
    );
}

async function matrix(method) {
    const rawA = document.getElementById("mA").value;
    const rawB = document.getElementById("mB").value;

    const matrixA = parseMatrix(rawA);
    const matrixB = rawB ? parseMatrix(rawB) : null;

    let payload = { matrixA };  // matches MatrixRequest.MatrixA

    if (method === "add") {
        payload.matrixB = matrixB;
    }

    // TODO: implement multiply with vector (code created using different sources)
    // if (method === "multiply") {
    //     const vecStr = prompt("Enter vector (e.g. 1,2):");
    //     const vector = vecStr.split(",").map(x => Number(x.trim()));
    //     payload.vector = vector;
    // }
    // I  believe this is logically correct, but my API does not support it yet

    const resp = await fetch(`${BASE}/linearalgebra/matrix/${method}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });

    const data = await resp.json();

    let result;
    switch (method) {
        case "determinant":
            result = data.determinant;
            break;
        case "transpose":
            result = data.transpose;
            break;
        case "add":
            result = data.result;
            break;
        case "multiply":
            result = data.matrixMultiply;
            break;
    }

    writeOutput("Result: " + JSON.stringify(result));
    appendHistory(`Matrix | ${method} = ${JSON.stringify(result)}`);
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
