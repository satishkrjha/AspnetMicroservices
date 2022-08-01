using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet("{ProductName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string ProductName)
        {
            var coupon = await _discountRepository.GetDiscount(ProductName);
            return Ok(coupon);
        }
        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CreateDiscount(Coupon coupon)
        {
            var affected = await _discountRepository.CreateDiscount(coupon);
            return Ok(affected);
        }
        [HttpPut]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateDiscount(Coupon coupon)
        {
            var affected = await _discountRepository.UpdateDiscount(coupon);
            return Ok(affected);
        }

        [HttpDelete("{ProductName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteDiscount(string ProductName)
        {
            var affected = await _discountRepository.DeleteDiscount(ProductName);
            return Ok(affected);
        }
    }
}
