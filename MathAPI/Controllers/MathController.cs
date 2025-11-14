using MathCore;
using MathNet.Numerics;
using Microsoft.AspNetCore.Mvc;

namespace MathAPI.Controllers
{
    // API controller for mathematical operations
    [ApiController]
    [Route("MathAPI")]
    public class MathController : ControllerBase
    {
        // GET endpoint to check if the API is running
        [HttpGet("Calculate")]
        public ContentResult ViewPage()
        {
            string html = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""UTF-8"">
                    <title>MathAPI Cloud Client</title>
                    <style>
                        body {
                            font-family: sans-serif;
                            margin: 2rem;
                        }

                        /* Main 2-column layout */
                        .container {
                            display: flex;
                            gap: 2rem;
                        }

                        .left {
                            width: 50%;
                        }

                        .right {
                            width: 50%;
                        }

                        textarea {
                            width: 100%;
                            height: 200px;
                            font-family: monospace;
                        }

                        input, select {
                            margin: 0.3rem;
                            width: 120px;
                        }

                        button {
                            margin: 0.3rem;
                        }
                    </style>
                </head>

                <body>

                    <h1>MathAPI Computational Models</h1>

                    <div class=""container"">
                        <div class=""left"">
                            <h2>Algebra</h2>

                            <label>A:</label>
                            <input id=""a"" type=""number"">
                            <label>B:</label>
                            <input id=""b"" type=""number"">

                            <br>
                            <button onclick=""algebra('add')"">Add</button>
                            <button onclick=""algebra('subtract')"">Subtract</button>
                            <button onclick=""algebra('multiply')"">Multiply</button>
                            <button onclick=""algebra('divide')"">Divide</button>

                            <h2>ODE Solver (Calculus)</h2>

                            <label>Function:</label>
                            <select id=""func"">
                                <option value=""tplusy"">t+y</option>
                                <option value=""linear"">2·y</option>
                                <option value=""sin"">sin(t)</option>
                                <option value=""exp"">exp(t)</option>
                                <option value=""quad"">y²</option>
                                <option value=""logistic"">logistic</option>
                                <option value=""harmonic"">-t</option>
                                <option value=""damped"">-0.3y</option>
                                <option value=""tcosy"">t·cos(y)</option>
                                <option value=""cubic"">y³-y</option>
                                <option value=""forced"">sin(t)-y</option>
                                <option value=""mix"">exp(t)+y</option>
                            </select>
                            <br>

                            <label>y0:</label>
                            <input id=""y0"" type=""number"">

                            <label>t0:</label>
                            <input id=""t0"" type=""number"">

                            <label>dt:</label>
                            <input id=""dt"" type=""number"">

                            <label>n:</label>
                            <input id=""n"" type=""number"">

                            <br>
                            <button onclick=""calc('Euler')"">Euler</button>
                            <button onclick=""calc('Heuns')"">Heuns</button>
                            <button onclick=""calc('RK2')"">RK2</button>
                            <button onclick=""calc('RK4')"">RK4</button>

                        </div>

                        <!-- RIGHT SIDE -->
                        <div class=""right"">
                            <h2>Output</h2>
                            <textarea id=""output"" readonly></textarea>

                            <h3>Output History</h3>
                            <textarea id=""history"" readonly></textarea>
                        </div>

                    </div>

                    <script>
                        // Change this when deployed
                        const BASE = ""http://localhost:5130"";


                        function writeOutput(text) {
                            const box = document.getElementById(""output"");
                            box.value = text;
                        }

                        function appendHistory(text) {
                            const h = document.getElementById(""history"");
                            h.value += text + ""\n"";
                        }

                        async function algebra(method) {
                            const a = parseFloat(document.getElementById(""a"").value);
                            const b = parseFloat(document.getElementById(""b"").value);

                            const resp = await fetch(`${BASE}/algebra/${method}`, {
                                method: ""POST"",
                                headers: { ""Content-Type"": ""application/json"" },
                                body: JSON.stringify({ a, b })
                            });

                            const data = await resp.json();
                            const text = `Algebra | ${method}(${a}, ${b}) = ${data.result}`;

                            writeOutput(""Result: "" + data.result);
                            appendHistory(""Result: "" + data.result);
                        }

                        async function calc(method) {
                            const func = document.getElementById(""func"").value;
                            const y0   = parseFloat(document.getElementById(""y0"").value);
                            const t0   = parseFloat(document.getElementById(""t0"").value);
                            const dt   = parseFloat(document.getElementById(""dt"").value);
                            const n    = parseInt(document.getElementById(""n"").value);

                            const resp = await fetch(`${BASE}/calculus/${method}`, {
                                method: ""POST"",
                                headers: { ""Content-Type"": ""application/json"" },
                                body: JSON.stringify({ func, y0, t0, dt, n })
                            });

                            const data = await resp.json();
                            const text = `ODE | ${method}(${func}, y0=${y0}, t0=${t0}, dt=${dt}, n=${n}) = ${data.result}`;

                            writeOutput(""Result: "" + data.result);
                            appendHistory(""Result: "" + data.result);
                        }
                    </script>
                </body>
                </html>";

            return Content(html, "text/html");
        }
    }
}
