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
        [HttpPost("api/item")]
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
        [HttpPatch("api/item/count")]
        public async Task<IActionResult> ModifyCountAsync(int count, string EAN)
        {
            try
            {
                var id = HttpContext.Request.Cookies["userId"].ToString();
                if (count == 0 || string.IsNullOrEmpty(id)) return BadRequest();

                var userId = Guid.Parse(id);
                var item = await _itemService.GetByEANAsync(EAN, userId);
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
        [HttpPut("api/item")]
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
        [HttpDelete("api/item")]
        public async Task<IActionResult> DeleteAsync(string EAN)
        {
            try
            {
                var id = HttpContext.Request.Cookies["userId"].ToString();
                if (string.IsNullOrEmpty(EAN) || string.IsNullOrEmpty(id)) return BadRequest();

                var userId = Guid.Parse(id);
                var item = await _itemService.GetByEANAsync(EAN, userId);
                if (item == null) return Ok();

                await _itemService.DeleteAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("api/item")]
        public async Task<IActionResult> GetByEANAsync(string EAN)
        {
            try
            {
                var id = HttpContext.Request.Cookies["userId"].ToString();
                if (string.IsNullOrEmpty(id)) return BadRequest();

                var userId = Guid.Parse(id);

                var item = await _itemService.GetByEANAsync(EAN, userId);
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
