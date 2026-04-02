const API_URL =
"http://localhost:5273/api/auth/login";

document
.getElementById("loginForm")
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

    // Client-side validation

    if (!username || !password)
    {
        showError(
            "All fields are required"
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
                "Invalid login"
            );
        }

        const data =
            await response.json();

        // Save JWT

        localStorage.setItem(
            "token",
            data.token
        );

        // Redirect

        window.location.href =
            "dashboard.html";

    }
    catch (error)
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