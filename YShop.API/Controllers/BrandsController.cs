using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YShop.core.IRepositories;
using YShop.core.Models;
using YShop.Infrastructure.Repositories;

namespace YShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var records = await _unitOfWork.Brand.GetAllAsync();
            return records;
        }
    }
}
