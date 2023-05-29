using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YShop.API.Helpers;
using YShop.core.IRepositories;
using YShop.core.Models;
using YShop.core.Specifications;
using YShop.core.ViewModels;

namespace YShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] ProductSpecParams param)
        //{
        //    //var records = await _unitOfWork.Product.GetAllAsync();
        //    //return records;

        //    //applay specification apttern
        //    var spec = new ProductWithBrandAndTypeSpec(param);
        //    var products = await _unitOfWork.Product.ListAllWithSpecAsync(spec);
        //    var productsDTo = _mapper.Map<IEnumerable<Product> , IEnumerable<ProductDto>>(products);
        //    return Ok(productsDTo);
        //}

        //SQLite does not support expressions of type 'decimal' in ORDER BY clauses.
        //Convert the values to a supported type, or use LINQ to Objects to order the results on
        //the client side."


        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductWithBrandAndTypeSpec(productParams);
            var countSpec = new ProductWithFiltersForCountSepecification(productParams);

            var totalItems = await _unitOfWork.Product.CountAsync(countSpec);

            var products = await _unitOfWork.Product.ListAllWithSpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            return Ok(new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }



        // GET: api/Products/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            //var records = await _unitOfWork.Product.GetFirstAsync(c => c.Id == id , "ProductType,ProductBrand");
            //return records;

            //applay specification apttern
            var spec = new ProductWithBrandAndTypeSpec(id);
            var product = await _unitOfWork.Product.GetEntityWithSpecAsync(spec);
            var productDto = _mapper.Map<Product, ProductDto>(product);
            return Ok(productDto);
        }
    }
}
