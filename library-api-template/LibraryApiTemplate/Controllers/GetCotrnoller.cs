﻿using LibraryCore.Model;
using LibraryDataBroker;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiTemplate.Controllers
{
    [Route("api/[controller]")]
    public abstract class GetController<TEntity> : ControllerBase, IGetController<TEntity> where TEntity : class, IDbRecord<TEntity>, new()
    {
        private readonly IGetDataBroker? repo;

        public GetController(IGetDataBroker repo)
        {
            this.repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBy(Guid id)
        {
            TEntity entity = new();
            if (repo is not null)
            {
                entity = await repo.GetByAsnyc<TEntity>(id);
                return Ok(entity);
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }
    }
}