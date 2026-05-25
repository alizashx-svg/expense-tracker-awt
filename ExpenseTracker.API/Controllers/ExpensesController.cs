using ExpenseTracker.API.Models;
using ExpenseTracker.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpensesController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<List<Expense>> Get()
        {
            return await _expenseService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Expense expense)
        {
            await _expenseService.CreateAsync(expense);

            return CreatedAtAction(nameof(Get), new { id = expense.Id }, expense);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _expenseService.RemoveAsync(id);

            return NoContent();
        }
    }
}