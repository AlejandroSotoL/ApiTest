 const form = document.getElementById("loginForm");
        const mensaje = document.getElementById("mensaje");
        const userRegex = /^[a-zA-Z0-9._-]{4,20}$/;
        const passRegex = /^[a-zA-Z0-9@#$%^&*!._-]{6,30}$/;
        form.addEventListener("submit", async (e) => {
            e.preventDefault();
            mensaje.textContent = "";
            form.querySelectorAll('span[data-for]').forEach(el => el.textContent = "");
            const data = {
                Username: form.querySelector('input[name="Username"]').value,
                Password: form.querySelector('input[name="Password"]').value
            };


            if (!userRegex.test(data.Username)) {
                const span = form.querySelector('span[data-for="Username"]');
                if (span) span.textContent = "Usuario inválido";
                return;
            }

            if (!passRegex.test(data.Password)) {
                const span = form.querySelector('span[data-for="Password"]');
                if (span) span.textContent = "Contraseña inválida";
                return;
            }
            try {
                const response = await fetch("/api/UsuarioApi/Login", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(data)
                });

                const result = await response.json();

                if (result.success) {
                    mensaje.innerHTML = result.message;
                    mensaje.className = "text-green-600 text-center mt-4 font-bold";
                } else {
                    mensaje.innerHTML = result.message || "Datos inválidos";
                    mensaje.className = "text-red-600 text-center mt-4";

                    if (result.errors) {
                        // 2. Mapear errores dinámicamente
                        result.errors.forEach(err => {
                            // Convertimos a minúsculas para comparar fácilmente
                            const errorMsg = err.toLowerCase();
                            
                            if (errorMsg.includes("username") || errorMsg.includes("usuario")) {
                                const span = form.querySelector('span[data-for="Username"]');
                                if (span) span.textContent = err;
                            }
                            if (errorMsg.includes("password") || errorMsg.includes("contraseña")) {
                                const span = form.querySelector('span[data-for="Password"]');
                                if (span) span.textContent = err;
                            }
                        });
                    }
                }

            } catch (error) {
                mensaje.textContent = "Error en la conexión";
                mensaje.className = "text-red-600 text-center mt-4";
            }
        });