using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IPlayersService _playerService;

        public HomeController(ICategoryService categoryService,
            IPlayersService playerService)
        {
            _categoryService = categoryService;
            _playerService = playerService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoryViewModel categoryViewModel)
        {
            _categoryService.Create(categoryViewModel);

            return Ok(categoryViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlayer(CancellationToken cancellationToken)
        {
            var result = await _playerService.GetAllPlayers(cancellationToken);

            return Ok(result);
        }


    }
}
