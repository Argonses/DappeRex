using Dapper;
using DappeRex.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DappeRex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IConfiguration _config;

        public InventoryController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllItems()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var items = await connection.QueryAsync<Item>("SELECT * FROM DappeRex");

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var item = await connection.QueryFirstOrDefaultAsync<Item>("SELECT * FROM DappeRex WHERE Id = @Id", new { Id = id });

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem(Item newItem)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var sql = "INSERT INTO DappeRex (Name, Description, Price) VALUES (@Name, @Description, @Price); SELECT CAST(SCOPE_IDENTITY() as int)";
            var itemId = await connection.QuerySingleAsync<int>(sql, newItem);

            newItem.Id = itemId;
            return CreatedAtAction(nameof(GetItemById), new { id = itemId }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Item>> UpdateItem(int id, Item updatedItem)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var sql = "UPDATE DappeRex SET Name = @Name, Description = @Description, Price = @Price WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id, updatedItem.Name, updatedItem.Description, updatedItem.Price });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var sql = "DELETE FROM DappeRex WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });

            return NoContent();
        }
    }
}
