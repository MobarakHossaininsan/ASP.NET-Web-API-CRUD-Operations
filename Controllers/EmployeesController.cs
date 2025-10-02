
using Employee.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(AppDbContext context) : ControllerBase
{
    // ✅ READ: Get all employees
    [HttpGet]
    public async Task<List<Models.Employee>> GetEmployees()
    {
        return await context.Employees.ToListAsync();
    }

    // ✅ READ: Get by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Models.Employee>> GetEmployee(int id)
    {
        var employee = await context.Employees.FindAsync(id);
        if (employee == null)
            return NotFound();
        return employee;
    }

    // ✅ CREATE: Add new employee
    [HttpPost]
    public async Task<ActionResult<Models.Employee>> CreateEmployee(Models.Employee employee)
    {
        context.Employees.Add(employee);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
    }

    // ✅ UPDATE: Update employee
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, Models.Employee employee)
    {
        if (id != employee.Id)
            return BadRequest();

        context.Entry(employee).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return NoContent();
    }

    // ✅ DELETE: Remove employee
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await context.Employees.FindAsync(id);
        if (employee == null)
            return NotFound();

        context.Employees.Remove(employee);
        await context.SaveChangesAsync();

        return NoContent();
    }
}