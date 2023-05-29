﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YShop.core.IRepositories;
using YShop.core.Models;
using YShop.Infrastructure.Repositories;

namespace YShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
        {
            var records = await _unitOfWork.Type.GetAllAsync();
            return records;
        }
    }
}
