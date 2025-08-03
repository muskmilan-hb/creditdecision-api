using CreditDecision.Models;
using LoanManager.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LoanManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepository _repo;

        public LoansController(ILoanRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _repo.GetAllAsync();
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await _repo.GetByIdAsync(id);
            if (loan == null) return NotFound();
            return Ok(loan);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Loan loan)
        {
            var created = await _repo.AddAsync(loan);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Loan loan)
        {
            if (id != loan.Id) return BadRequest();

            var updated = await _repo.UpdateAsync(loan);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
