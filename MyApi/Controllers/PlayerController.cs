using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApi.Contexts;
using MyApi.Data;
using MyApi.Entities;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class PlayerController : ControllerBase
    {
        //private readonly ApplicationDbContext context;
        private readonly PlayerRepository repository;

        public PlayerController(PlayerRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> Get()
        {
            return await repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            var player = await repository.GetById(id);

            if(player == null)
            {
                return NotFound();
            }

            return player;
        }

        [HttpPost]
        public async Task Post([FromBody] Player player)
        {
            await repository.Insert(player);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await repository.DeleteById(id);
        }
    }
}
