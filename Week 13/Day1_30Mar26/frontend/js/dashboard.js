const API_BASE =
"http://localhost:5273/api/transactions";

let currentPage = 1;
const pageSize = 5;

window.onload = function ()
{
    checkAuth();
    loadTransactions();
};

function checkAuth()
{
    const token =
        localStorage.getItem("token");

    if (!token)
    {
        window.location.href =
            "login.html";
    }
}

async function loadTransactions()
{
    const token =
        localStorage.getItem("token");

    const type =
        document
        .getElementById("filterType")
        ?.value || "";

    const sortBy =
        document
        .getElementById("sortBy")
        ?.value || "date";

    let url =
        API_BASE +
        `?pageNumber=${currentPage}` +
        `&pageSize=${pageSize}` +
        `&type=${type}` +
        `&sortBy=${sortBy}`;

    try {

        const response =
            await fetch(url,
        {
            headers:
            {
                "Authorization":
                    "Bearer " + token
            }
        });

        const data =
            await response.json();

        const table =
            document.getElementById(
                "transactionTable"
            );

        table.innerHTML = "";

        data.forEach(t =>
        {
            const row =
                `<tr>
                    <td>${t.amount}</td>
                    <td>${formatDate(t.date)}</td>
                    <td>${t.type}</td>
                </tr>`;

            table.innerHTML += row;
        });

        document
            .getElementById("pageNumber")
            .textContent =
            currentPage;

    }
    catch(error)
    {
        console.error(error);
    }
}

function applyFilters()
{
    currentPage = 1;
    loadTransactions();
}

function nextPage()
{
    currentPage++;
    loadTransactions();
}

function prevPage()
{
    if (currentPage > 1)
    {
        currentPage--;
        loadTransactions();
    }
}

document
.getElementById("transactionForm")
.addEventListener("submit",
async function(e)
{
    e.preventDefault();

    const amount =
        document
        .getElementById("amount")
        .value;

    const type =
        document
        .getElementById("type")
        .value;

    if (amount <= 0)
    {
        alert(
            "Amount must be greater than 0"
        );
        return;
    }

    const token =
        localStorage.getItem("token");

    try {

        await fetch(
            API_BASE,
        {
            method: "POST",

            headers:
            {
                "Content-Type":
                    "application/json",

                "Authorization":
                    "Bearer " + token
            },

            body: JSON.stringify({
                amount,
                type
            })
        });

        loadTransactions();

        document
            .getElementById(
                "transactionForm"
            )
            .reset();

    }
    catch(error)
    {
        console.error(error);
    }

});

function logout()
{
    localStorage.removeItem("token");

    window.location.href =
        "login.html";
}

function formatDate(dateString)
{
    const date =
        new Date(dateString);

    return date.toLocaleString();
}