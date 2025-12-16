console.log("RUNNING main.js v2025-12-16-1");

/* BASE URL */
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

/* OUTPUT & HISTORY HANDLING */
function writeOutput(text) {
    document.getElementById("output").value = text;
}

/* APPEND TO HISTORY */
function appendHistory(text) {
    const h = document.getElementById("history");
    h.value += text + "\n";
    h.scrollTop = h.scrollHeight;
}

/* POST JSON FUNCTION */
async function postJson(url, payload) {
  const resp = await fetch(url, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload)
  });

  let data = null;
  try { data = await resp.json(); } catch { /* ignore */ }

  if (!resp.ok) {
    const msg = (data && (data.message ?? data.error)) ?? `HTTP ${resp.status}`;
    throw new Error(msg);
  }
  return data;
}

/* ALGEBRA API */

async function algebra(method) {
    const a = Number(aInput.value);
    const b = Number(bInput.value);

    if (!Number.isFinite(a) || !Number.isFinite(b)) {
        const msg = "Error: a and b must be valid numbers.";
        writeOutput(msg);
        return;
    }

    try {
        const data = await postJson("/algebra/add", { a, b });
        writeOutput(`Result: ${data.result}`);
        appendHistory(`Algebra | ${method}(${a}, ${b}) => ${data.result}`);
    } catch (e) {
        const msg = e.message ?? "Unknown error";
        writeOutput(`Error: ${msg}`);
        appendHistory(`Algebra | ${method}(${a}, ${b}) => Error: ${msg}`);
    }
}

/* CALCULUS API */

async function calc(method) {
    const func = funcInput.value.trim();
    const y0 = Number(y0Input.value);
    const t0 = Number(t0Input.value);
    const dt = Number(dtInput.value);
    const n  = Number(nInput.value);

    if (!func || !Number.isFinite(y0) || !Number.isFinite(t0) || !Number.isFinite(dt) || !Number.isFinite(n)) {
        const msg = "Error: func, y0, t0, dt, n are required.";
        writeOutput(msg);
        appendHistory(`ODE | ${method.toUpperCase()}, ${func || "(missing)"} => ${msg}`);
        return;
    }
    if (dt <= 0 || n < 1) {
        const msg = "Error: dt must be > 0 and n must be >= 1.";
        writeOutput(msg);
        appendHistory(`ODE | ${method.toUpperCase()}, ${func} => ${msg}`);
        return;
    }

    try {
        const data = await postJson(`/calculus/${method}`, { func, y0, t0, dt, n });
        writeOutput(`Result: ${JSON.stringify(data.result)}`);
        appendHistory(`ODE | ${method.toUpperCase()}, ${func} => ${JSON.stringify(data.result)}`);
    } catch (e) {
        const msg = e.message ?? "Unknown error";
        writeOutput(`Error: ${msg}`);
        appendHistory(`ODE | ${method.toUpperCase()}, ${func} => Error: ${msg}`);
    }
}

/* VECTOR API */

function parseVector(str) {
  const s = (str ?? "").trim();
  if (!s) return null;

  const arr = s.split(",").map(x => Number(x.trim()));
  if (arr.length === 0 || arr.some(v => !Number.isFinite(v))) return null;
  return arr;
}

async function vector(method) {
    const A = parseVector(vecAInput.value);
    const B = parseVector(vecBInput.value);

    if (!A || !B) {
        const msg = "Error: invalid vector input (use comma-separated numbers).";
        writeOutput(msg);
        appendHistory(`Vector | ${method} => ${msg}`);
        return;
    }
    if (A.length !== B.length) {
        const msg = "Error: vectors must be the same length.";
        writeOutput(msg);
        appendHistory(`Vector | ${method} => ${msg}`);
        return;
    }

    try {
        const data = await postJson(`/linearalgebra/vector/${method}`, { vectorA: A, vectorB: B });
        writeOutput(`Result: ${JSON.stringify(data.result)}`);
        appendHistory(`Vector | ${method} => ${JSON.stringify(data.result)}`);
    } catch (e) {
        const msg = e.message ?? "Unknown error";
        writeOutput(`Error: ${msg}`);
        appendHistory(`Vector | ${method} => Error: ${msg}`);
    }
}

/* MATRIX API */

function parseMatrix(str) {
  const s = (str ?? "").trim();
  if (!s) return null;

  const M = s.split(";").map(row => row.split(",").map(x => Number(x.trim())));
  if (M.length === 0) return null;

  const cols = M[0].length;
  if (cols === 0) return null;

  if (M.some(r => r.length !== cols)) return null;                 // rectangular
  if (M.some(r => r.some(v => !Number.isFinite(v)))) return null;  // numeric only
  return M;
}

async function matrix(method) {
    const rawA = document.getElementById("mA").value;
    const operandType = document.getElementById("opType").value;

    const matrixA = parseMatrix(rawA);
    if (!matrixA) {
        writeOutput("Error: Invalid Matrix A");
        return;
    }
    let payload = { matrixA };  // default payload

    if (method === "add") {
        if (operandType !== "matrix") {
             writeOutput("Error: Matrix Addition requires a Matrix B");
            return;
        }
        payload.matrixB = parseMatrix(document.getElementById("mB").value);
    }

    if (method === "multiply") {
        if (operandType === "matrix") {
            payload.matrixB = parseMatrix(document.getElementById("mB").value);
            
        } else {
            payload.vector = parseVector(document.getElementById("mVec").value);
        }
    }

    try {
        const data = await postJson(`/linearalgebra/matrix/${method}`, payload);
        if (!("result" in data)) {
            throw new Error("Malformed response from API");
        }
        writeOutput("Result: " + JSON.stringify(data.result));
        appendHistory(`Matrix | ${method} => ${JSON.stringify(data.result)}`);
    } catch (e) {
        const msg = e.message ?? "Unknown error";
        writeOutput("Error: " + msg);
        appendHistory(`Matrix | ${method} => Error: ${msg}`);
    }
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
