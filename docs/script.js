const API_URL = "http://localhost:8080/api/Expenses";

const form = document.getElementById("expenseForm");
const expenseList = document.getElementById("expenseList");
const totalAmount = document.getElementById("totalAmount");

form.addEventListener("submit", addExpense);

// ADD EXPENSE
async function addExpense(e) {
    e.preventDefault();

    const expense = {
        title: document.getElementById("title").value,
        amount: parseFloat(document.getElementById("amount").value),
        category: document.getElementById("category").value,
        date: document.getElementById("date").value
    };

    try {
        const response = await fetch(API_URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(expense)
        });

        if (response.ok) {
            form.reset();
            loadExpenses();
        }

    } catch (error) {
        console.log(error);
    }
}

// LOAD EXPENSES
async function loadExpenses() {

    const response = await fetch(API_URL);

    const expenses = await response.json();

    expenseList.innerHTML = "";

    let total = 0;

    expenses.forEach(expense => {

        total += expense.amount;

        expenseList.innerHTML += `
            <div class="expense-card">
                <div class="expense-info">
                    <h3>${expense.title}</h3>
                    <p>${expense.category}</p>
                    <small>${expense.date.split("T")[0]}</small>
                </div>

                <div class="expense-right">
                    <h2>₹${expense.amount}</h2>
                </div>
            </div>
        `;
    });

    totalAmount.innerText = total;
}

// INITIAL LOAD
loadExpenses();