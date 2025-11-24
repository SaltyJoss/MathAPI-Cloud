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

    writeOutput("Result: " + data.result);
    appendHistory(`Algebra | ${method}(${a}, ${b}) => ${JSON.stringify(data.result)}`);
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

    writeOutput("Result: " + data.result);
    appendHistory(`ODE | ${method} ${func} => ${JSON.stringify(data.result)}`);
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

    writeOutput("Result: " + data.result); 
    appendHistory(`Vector | ${method} => ${JSON.stringify(data.result)}`);
}

/* MATRIX API */

function parseMatrix(str) {
    return str.split(";").map(row =>
        row.split(",").map(n => Number(n.trim()))
    );
}

async function matrix(method) {
    const rawA = document.getElementById("mA").value;
    const operandType = document.getElementById("opType").value;

    const matrixA = parseMatrix(rawA);
    let payload = { matrixA };  // default payload

    if (method === "add") {
        if (operandType !== "matrix") {
             writeOutput("Error: Matrix Addition requires a Matrix B");
            return;
        }
        payload.matrixB = parseMatrix(document.getElementById("mB").value);
    }
    // if (method === "multiply") {
    //     const vecStr = prompt("Enter vector (e.g. 1,2):");
    //     const vector = vecStr.split(",").map(x => Number(x.trim()));
    //     payload.vector = vector;
    // }
    // I  believe this is logically correct, but my API does not support it yet

    if (method === "multiply") {
        if (operandType === "matrix") {
            payload.matrixB = parseMatrix(document.getElementById("mB").value);
            
        } else {
            payload.vector = parseVector(document.getElementById("mVec").value);
        }
    }

    const resp = await fetch(`${BASE}/linearalgebra/matrix/${method}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });

    const data = await resp.json();
    const result = data.result;

    writeOutput("Result: " + JSON.stringify(result));
    appendHistory(`Matrix | ${method} => ${JSON.stringify(result)}`);
}

// UI Interactions
document.addEventListener("DOMContentLoaded", () => {

    // COLLAPSIBLE MODEL PANELS
    const titles = document.querySelectorAll(".model-title");

    titles.forEach(t => {
        t.addEventListener("click", () => {
            const block = t.closest(".model-block");
            block.classList.toggle("collapsed");
        });
    });

    // MATRIX OPERAND SWITCH
    const op = document.getElementById("opType");
    const matB = document.getElementById("matB-block");
    const vec = document.getElementById("vec-block");

    console.log("Loaded:", op, matB, vec); // debugging so I can see if elements are found (or NOT)

    if (op && matB && vec) {
        op.addEventListener("change", () => {
            if (op.value === "vector") {
                matB.style.display = "none";
                vec.style.display = "block";
            } else {
                matB.style.display = "block";
                vec.style.display = "none";
            }
        });
    } else {
        console.warn("[WARN] Matrix operand DOM elements NOT FOUND."); // more debugging 
    }
});
