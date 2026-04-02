const API_URL =
"http://localhost:5273/api/auth/register";

document
.getElementById("registerForm")
.addEventListener("submit",
async function(e) {

    e.preventDefault();

    const username =
        document
        .getElementById("username")
        .value.trim();

    const password =
        document
        .getElementById("password")
        .value.trim();

    // Validation

    if (username.length < 3)
    {
        showError(
            "Username too short"
        );
        return;
    }

    if (password.length < 5)
    {
        showError(
            "Password too short"
        );
        return;
    }

    try {

        const response =
            await fetch(API_URL,
        {
            method: "POST",

            headers:
            {
                "Content-Type":
                "application/json"
            },

            body: JSON.stringify({
                username,
                password
            })
        });

        if (!response.ok)
        {
            throw new Error(
                "Registration failed"
            );
        }

        const data =
            await response.json();

        localStorage.setItem(
            "token",
            data.token
        );

        window.location.href =
            "dashboard.html";

    }
    catch(error)
    {
        showError(error.message);
    }

});

function showError(message)
{
    document
    .getElementById("errorMessage")
    .textContent = message;
}