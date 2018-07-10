using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using SWAG.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWAG.Controllers
{
    [ApiVersion("1.0")]
    public class OperationsController
        : Mvc.ControllerBase<OperationsController>
    {
        public OperationsController(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            OperationEntity[] entities = new OperationEntity[0];

            Byte[] result = await Cache.GetAsync("operations");
            if ((result?.Length ?? 0) > 0)
            {
                entities = EntityExtensions.GetEntities<OperationEntity[]>(result);

                var todayEntities = await Context.Operations
                    .Where(e => e.CreatedOn.HasValue && e.CreatedOn.Value.Date >= DateTime.UtcNow.Date)
                    .ToArrayAsync();

                if ((todayEntities?.Count() ?? 0) > 0)
                {
                    entities = entities.Union(todayEntities).ToArray();
                }
            }
            else
            {
                entities = await Context.Operations.ToArrayAsync();

                var olderEntities = entities
                    .Where(e => !e.CreatedOn.HasValue || e.CreatedOn.Value.Date < DateTime.UtcNow.Date)
                    .ToArray();

                if ((olderEntities?.Count() ?? 0) > 0)
                {
                    await Cache.SetAsync("operations", olderEntities.ToByteArray(), new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.Date.AddDays(1),
                    });
                }
            }

            return new ObjectResult(Mapper.Map<IEnumerable<OperationModel>>(entities));
        }

        [HttpGet("{id}", Name = "GetOperation")]
        public async Task<IActionResult> GetById(Guid? id)
        {
            if (!id.HasValue)
            {
                Logger.LogWarning("Wrong identifier!");

                return BadRequest();
            }

            OperationEntity entity = null;

            Byte[] result = await Cache.GetAsync($"operation-{id}");
            if ((result?.Length ?? 0) > 0)
            {
                entity = EntityExtensions.GetEntity<OperationEntity>(result);
            }
            else
            {
                entity = await Context.Operations.FirstOrDefaultAsync(o => o.Id == id.Value);

                if (entity != null)
                {
                    await Cache.SetAsync($"operation-{id}", entity.ToByteArray());
                }
            }

            if (entity == null)
            {
                Logger.LogWarning($"Entity with id: '{id.Value}' does not exists!");

                return NotFound();
            }

            return new ObjectResult(Mapper.Map<OperationModel>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OperationModel value)
        {
            if (!ModelState.IsValid)
            {
                foreach (ModelError error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Logger.LogWarning(error.Exception, error.ErrorMessage);
                }

                return BadRequest();
            }

            if (value == null || value.Type == OperationType.None || (value.Value?.Length ?? 0) == 0)
            {
                Logger.LogWarning("Wrong one or more value(s)!");

                return BadRequest();
            }

            try
            {
                OperationEntity entity = Mapper.Map<OperationEntity>(value);

                EntityEntry<OperationEntity> result = await Context.Operations
                    .AddAsync(entity);
                await Context.SaveChangesAsync();

                value = Mapper.Map<OperationModel>(result.Entity);

                return CreatedAtRoute("GetOperation", new { id = value.Id }, value);
            }
            catch (AutoMapperMappingException ex)
            {
                if (ex?.InnerException is DivideByZeroException)
                {
                    Logger.LogWarning(ex, "Divide by zero!");

                    return BadRequest();
                }

                throw;
            }
            catch (DivideByZeroException ex)
            {
                Logger.LogWarning(ex, "Divide by zero!");

                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}