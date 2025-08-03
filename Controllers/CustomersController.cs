using CreditDecision.Models;
using CreditDecision.Repositories;
using CreditDecision.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController(ICustomerRepository repo, CustomerService customerService) : ControllerBase
    {
        private readonly ICustomerRepository _repo = repo;
        private readonly CustomerService _customerService = customerService;


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _repo.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            var created = await _repo.AddAsync(customer);
            try
            {
                await _customerService.SendCustCreatedMailAsync(created);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);      
            }
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Customer customer)
        {
            if (id != customer.Id) return BadRequest();

            var updated = await _repo.UpdateAsync(customer);
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
