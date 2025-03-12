using EstoqFy.Core.Contracts.Services;
using EstoqFy.Core.Entities;
using EstoqFy.Database;
using Microsoft.AspNetCore.Mvc;

namespace EstoqFy.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpPost("item")]
        public async Task<IActionResult> CreateAsync([FromBody] Item item)
        {
            try
            {
                if (item is null) return BadRequest();
                var exists = await _itemService.ExistsAsync(item.EAN, item.UserId);
                if (exists) return Ok();

                item.Count = item.Count == 0 ? 1 : item.Count;
                item.LastAdd = DateTime.Now;

                await _itemService.CreateAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("item/count")]
        public async Task<IActionResult> ModifyCountAsync(int count, Guid id)
        {
            try
            {
                var item = await _itemService.GetByIdAsync(id);
                item.Count = count;
                item.LastAdd = DateTime.Now;
                await _itemService.UpdateAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("item")]
        public async Task<IActionResult> UpdateAsync([FromBody] Item item)
        {
            try
            {
                if (item is null) return BadRequest();

                var exists = await _itemService.ExistsAsync(item.EAN, item.UserId);
                if (!exists) return NotFound();
                item.LastAdd = DateTime.Now;
                await _itemService.UpdateAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("item")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var item = await _itemService.GetByIdAsync(id);
                if (item == null) return Ok();

                await _itemService.DeleteAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("item")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var item = await _itemService.GetByIdAsync(id);
                if (item == null) return NotFound();

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("items")]
        public async Task<IActionResult> GetAllAsync(Guid userId)
        {
            try
            {
                var item = await _itemService.GetAllAsync(userId);
                if (item == null) return NotFound();

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
